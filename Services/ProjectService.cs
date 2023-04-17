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

        public async Task<ActionResult<IEnumerable<Project>>> GetProject() {
            if (_context.Project == null)
            {
                return NotFound();
            }
            return await _context.Project.ToListAsync();
        }

        public async Task<ActionResult<Project>> GetProjectById(int id) {
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

        public async Task<IActionResult> UpdateProject(int id, Project project) {
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

        public async Task<ActionResult<Project>> AddProject(Project project) {
            if (_context.Project == null)
            {
                return Problem("Entity set 'SolarSystemsDbContext.Project'  is null.");
            }
            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        public async Task<IActionResult> DeleteProject(int id) {
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

        public int GetPriceEstimate(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Project>> CreateNewProject(int projectExpertId, string ProjectDescription, string ProjectLocation, string CustomerName, int HourlyLaborRate, int LaborTime)
        {
            UserService userService = new UserService(_context);
            Project newProject = new Project
            {
                ProjectDescription = ProjectDescription,
                ProjectLocation= ProjectLocation,
                CustomerName = CustomerName,
                HourlyLaborRate = HourlyLaborRate,
                LaborTime = LaborTime
            };

            //set expert id in user table
            var expert = await userService.GetUserById(projectExpertId);
            if(expert == null)
            {
                return NotFound();
            }
            else
            {
                if (expert.Value.Projects == null)
                {
                    // Initialize the Projects collection of chosen expert if it's null
                    expert.Value.Projects = new List<Project>();
                }
                expert.Value.Projects.Add(newProject);
                await userService.UpdateUser(expert.Value.Id, expert.Value);
            }

            _context.Project.Add(newProject);
            await _context.SaveChangesAsync();

            return newProject;
        }

        public async Task<ActionResult<Project>> AddComponentToProject(int componentId, int componentQuantity, int projectId)
        {
            ContainerService containerService = new ContainerService(_context);
            ComponentService componentService = new ComponentService(_context);
            ProjectStatusService projectStatusService = new ProjectStatusService(_context);
            var component = await componentService.GetComponentById(componentId); //find component by id
            var project = await this.GetProjectById(projectId) ; //find project by id
            var numberOfComponentsAvailable = containerService.NumberOfAvailableComponentsById(component.Value.Id); //calculate how many pieces of component is available currently
            //if there is not enough ordering is possible
            if(numberOfComponentsAvailable < componentQuantity)
            {
                //todo
            }
            else
            {
                //assign components to project
                if (project.Value.Components == null)
                {
                    // Initialize the Projects collection of chosen expert if it's null
                    project.Value.Components = new List<Component>();
                }
                project.Value.Components.Add(component.Value);
                await this.UpdateProject(projectId, project.Value);
                var projectStatus = await projectStatusService.GetProjectStatusByProjectId(projectId);
                await projectStatusService.AddProjectStatusWithProjectId(projectId);
            }
            //remove component that is to be used from the warehouse if avialable
            //todo
            //project status changes to draft
            return project.Value;
        }
    }
}
