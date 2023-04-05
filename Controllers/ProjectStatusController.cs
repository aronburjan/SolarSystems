using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarSystems.Models;

namespace SolarSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectStatusController : ControllerBase
    {
        private readonly SolarSystemsDbContext _context;

        public ProjectStatusController(SolarSystemsDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectStatus>>> GetProjectStatus()
        {
          if (_context.ProjectStatus == null)
          {
              return NotFound();
          }
            return await _context.ProjectStatus.ToListAsync();
        }

        // GET: api/ProjectStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectStatus>> GetProjectStatus(int id)
        {
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

        // PUT: api/ProjectStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectStatus(int id, ProjectStatus projectStatus)
        {
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

        // POST: api/ProjectStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectStatus>> PostProjectStatus(ProjectStatus projectStatus)
        {
          if (_context.ProjectStatus == null)
          {
              return Problem("Entity set 'SolarSystemsDbContext.ProjectStatus'  is null.");
          }
            _context.ProjectStatus.Add(projectStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectStatus", new { id = projectStatus.Id }, projectStatus);
        }

        // DELETE: api/ProjectStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectStatus(int id)
        {
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

        private bool ProjectStatusExists(int id)
        {
            return (_context.ProjectStatus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
