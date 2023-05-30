using System;
using System.Collections.Generic;
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
    public class ProjectComponentsController : ControllerBase
    {
        private readonly IProjectComponentService _context;

        public ProjectComponentsController(IProjectComponentService context)
        {
            _context = context;
        }

        

        // GET: api/ProjectComponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProjectComponent>>> GetProjectComponent(int id)
        {

            var projectComponent = await _context.GetProjectComponent(id);


            return projectComponent;
        }

        
    }
}
