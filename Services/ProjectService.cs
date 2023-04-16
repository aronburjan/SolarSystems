using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using SolarSystems.Models;

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

        public async Task<ActionResult<Project>> CreateNewProject(int projectExpertId, string ProjectDescription, string CustomerName, int HourlyLaborRate, int LaborTime)
        {
            UserService userService = new UserService(_context);
            Project newProject = new Project
            {
                ProjectDescription = ProjectDescription,
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
    }
}
