using SolarSystems.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SolarSystems.Services
{
    public class ProjectStatusService : ControllerBase, IProjectStatusService
    {
        private readonly SolarSystemsDbContext _context;

        public ProjectStatusService(SolarSystemsDbContext context)
        {

            _context = context;
        }
        public async Task<ActionResult<IEnumerable<ProjectStatus>>> GetProjectStatus() {
            if (_context.ProjectStatus == null)
            {
                return NotFound();
            }
            return await _context.ProjectStatus.ToListAsync();
        }

        public async Task<ActionResult<ProjectStatus>> GetProjectStatusById(int id) {
            if (_context.ProjectStatus == null)
            {
                return NotFound();
            }
            var projectStatus = await _context.ProjectStatus.FindAsync(id);

            if (projectStatus == null)
            {
                return NotFound();
            }

            return projectStatus;
        }

        public async Task<IActionResult> UpdateProjectStatus(int id, ProjectStatus projectStatus) {
            if (id != projectStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectStatusExists(id))
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

        public async Task<ActionResult<ProjectStatus>> AddProjectStatus(ProjectStatus projectStatus) {
            if (_context.ProjectStatus == null)
            {
                return Problem("Entity set 'SolarSystemsDbContext.ProjectStatus'  is null.");
            }
            _context.ProjectStatus.Add(projectStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectStatusById", new { id = projectStatus.Id }, projectStatus);
        }

        public async Task<IActionResult> DeleteProjectStatus(int id) {
            if (_context.ProjectStatus == null)
            {
                return NotFound();
            }
            var projectStatus = await _context.ProjectStatus.FindAsync(id);
            if (projectStatus == null)
            {
                return NotFound();
            }

            _context.ProjectStatus.Remove(projectStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public bool ProjectStatusExists(int id)
        {
            return (_context.ProjectStatus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
