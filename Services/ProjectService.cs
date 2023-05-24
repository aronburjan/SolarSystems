using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using SolarSystems.Models;
using System.Composition;

namespace SolarSystems.Services
{
    public class ProjectService : ControllerBase, IProjectService
    {
        private readonly SolarSystemsDbContext _context;

        public ProjectService(SolarSystemsDbContext context)
        {

            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Project>>> GetProject()
        {
            if (_context.Project == null)
            {
                return NotFound();
            }
            return await _context.Project.ToListAsync();
        }

        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            if (_context.Project == null)
            {
                return NotFound();
            }
            var project = await _context.Project.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public async Task<ActionResult<Project>> AddProject(Project project)
        {
            if (_context.Project == null)
            {
                return Problem("Entity set 'SolarSystemsDbContext.Project'  is null.");
            }
            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        public async Task<IActionResult> DeleteProject(int id)
        {
            if (_context.Project == null)
            {
                return NotFound();
            }
            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public bool ProjectExists(int id)
        {
            return (_context.Project?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<int> GetComponentsPriceEstimate(int id)
        {
            int totalPrice = 0;
            ProjectComponentService projectComponentService = new ProjectComponentService(_context);
            ComponentService componentService = new ComponentService(_context);
            var projectComponents = await _context.ProjectComponent.ToListAsync();
            var project = this.GetProjectById(id);
            //add component prices to total price
            for (int i = 0; i < projectComponents.Count; i++)
            {
                if (projectComponents[i].ProjectId == id)
                {
                    var newComponent = componentService.GetComponentById(projectComponents[i].ComponentId);
                    totalPrice += newComponent.Result.Value.price * projectComponents[i].Quantity;
                }
            }
            /*foreach(var projectComponentPair in projectComponents.Value)
            {
                if(projectComponentPair.ProjectId == id)
                {
                    var newComponent = componentService.GetComponentById(projectComponentPair.ComponentId);
                    totalPrice += newComponent.Result.Value.price * projectComponentPair.Quantity;
                }
            }*/

            return totalPrice;
        }

        public async Task<ActionResult<Project>> CreateNewProject(string ProjectDescription, string ProjectLocation, string CustomerName, int HourlyLaborRate, int LaborTime)
        {
            ProjectStatusService projectStatusService = new ProjectStatusService(_context);
            Project newProject = new Project
            {
                ProjectDescription = ProjectDescription,
                ProjectLocation = ProjectLocation,
                CustomerName = CustomerName,
                HourlyLaborRate = HourlyLaborRate,
                LaborTime = LaborTime
            };

            //set expert id in user table
            _context.Project.Add(newProject);
            await _context.SaveChangesAsync();
            await projectStatusService.AddProjectStatusWithProjectId(newProject.Id);
            return newProject;
        }

        public async Task<ActionResult<Project>> AddComponentToProject(int componentId, int componentQuantity, int projectId)
        {
            ContainerService containerService = new ContainerService(_context);
            ComponentService componentService = new ComponentService(_context);
            ProjectComponentService projectComponentService = new ProjectComponentService(_context);
            ProjectStatusService projectStatusService = new ProjectStatusService(_context);
            var component = await componentService.GetComponentById(componentId); //find component by id
            var project = await this.GetProjectById(projectId); //find project by id
            var numberOfComponentsAvailable = containerService.NumberOfAvailableComponentsById(component.Value.Id); //calculate how many pieces of component is available currently
            int totalPrice = 0;
            //if there is not enough ordering is possible
            //nincs elég komponens -> az árkalkuláció ettől függetlenül elkészülhet, de a projekt
            //nem kerülhet scheduled statusba
            //így a projekt wait statusba kerül addig, amíg meg nem érkeznek az alkatrészek
            //assign components to project
            if (project.Value.Components == null)
            {
                // Initialize the Projects collection of chosen expert if it's null
                project.Value.Components = new List<Component>();
            }

            project.Value.Components.Add(component.Value);
            var projectComponent = new ProjectComponent();
            projectComponent.Project = project.Value;
            projectComponent.Component = component.Value;
            projectComponent.Quantity = componentQuantity;
            await projectComponentService.AddProjectComponent(projectComponent);
            totalPrice = await GetComponentsPriceEstimate(project.Value.Id);
            if (project.Value.totalPrice == 0)
            {
                totalPrice += project.Value.HourlyLaborRate * project.Value.LaborTime;
            }
            project.Value.totalPrice += totalPrice;
            await this.UpdateProject(projectId, project.Value);
            var projectStatus = await projectStatusService.GetProjectStatusByProjectId(projectId);
            await projectStatusService.AddProjectStatusWithProjectId(projectId);
            if (componentQuantity <= numberOfComponentsAvailable)
            {
                await updateStatus(projectId, "Scheduled");
            }
            else
            {
                await updateStatus(projectId, "Wait");
            }

            projectStatus.Value.DateTime = DateTime.Now.TimeOfDay.ToString();
            await projectStatusService.UpdateProjectStatus(projectStatus.Value.Id, projectStatus.Value);



            //remove component that is to be used from the warehouse if avialable
            //todo
            //project status changes to draft

            return project.Value;
        }

        public async Task updateStatus(int id, string status)
        {
            ProjectStatusService projectStatusService = new ProjectStatusService(_context);
            var project = await this.GetProjectById(id);
            var projectStatus = await projectStatusService.GetProjectStatusByProjectId(id);
            if (project == null)
            {
                return;
            }
            else
            {
                project.Value.CurrentStatus = status;
                projectStatus.Value.status = status;
            }
            return;
        }

        public async Task<ActionResult<IEnumerable<Project>>> getProjectsByStatus(string status)
        {
            var projects = await _context.Project.Where(p => p.CurrentStatus.Equals(status)).ToListAsync();
            if(projects != null)
            {
                return projects;
            }
            return NoContent();
        }
    }
}