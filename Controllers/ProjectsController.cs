using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarSystems.Models;
using SolarSystems.Services;

namespace SolarSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _context;

        public ProjectsController(IProjectService context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProject()
        {
            return await _context.GetProject();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project =  await _context.GetProjectById(id);
            return project;
           
        }

        [HttpGet("/status/{status}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByStatus(string status)
        {
            return await _context.getProjectsByStatus(status);
        }

        [HttpGet("/estimate/{id}")]
        public async Task estimatePrice(int id)
        {
            await _context.estimatePrice(id);
            return;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            var _project = await _context.UpdateProject(id, project);
            return _project;
            
        }

        [HttpPost("take/components/for/{id}")]
        public async Task takeComponentsForProject(int id)
        {
            await _context.getComponentsForProject(id);
            return;
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            var _project = await _context.AddProject(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpPost("{ProjectDescription}/{ProjectLocation}/{CustomerName}/{HourlyLaborRate}/{LaborTime}")]
        public async Task<ActionResult<Project>> CreateNewProject( string ProjectDescription, string ProjectLocation, string CustomerName, int HourlyLaborRate, int LaborTime)
        {
            var _project = await _context.CreateNewProject(ProjectDescription, ProjectLocation, CustomerName, HourlyLaborRate, LaborTime);
            return CreatedAtAction(nameof(GetProject), new { id = _project.Value.Id }, _project);
        }

        //FIXÁLNI KÉNE!!!!!!!!!!!!!!!!!!!!
        [HttpPost("{componentId}/{componentQuantity}/{projectId}")]
        public async Task<ActionResult<Project>> AddComponentToProject(int componentId, int componentQuantity, int projectId)
        {
            var _project = await _context.AddComponentToProject(componentId, componentQuantity, projectId);
            return NoContent();
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var _project = await _context.DeleteProject(id);
            return _project;
        }

       
    }
}
