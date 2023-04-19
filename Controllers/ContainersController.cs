using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarSystems.Models;
using SolarSystems.Services;


namespace SolarSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainersController : ControllerBase
    {
        private readonly IContainerService _context;

        public ContainersController(IContainerService context)
        {
            _context = context;
        }

        // GET: api/Containers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Container>>> GetContainer()
        {

            return await _context.GetContainer();
        }

        // GET: api/Containers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Container>> GetContainer(int id)
        {
            var container = await _context.GetContainerById(id);

            return container;
        }

        // GET by row and column and number
        [HttpGet("{containerRow}/{containerColumn}/{containerNumber}")]
        public async Task<ActionResult<Container>> GetContainerNumber(int containerRow, int containerColumn, int containerNumber)
        {
            var container = await _context.GetContainerNumber(containerRow, containerColumn, containerNumber);
            return container;
        }

        // GET available components
        [HttpGet("/available")]
        public async Task<ActionResult<IEnumerable<Component>>> GetAvailableComponents()
        {
            var componentList = await _context.ListAvailableComponents();
            return componentList;
        }


        // PUT: api/Containers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContainer(int id, Container container)
        {
            var _container = await _context.UpdateContainer(id, container);
            return _container;
        }

        // POST: api/Containers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Container>> PostContainer(Container container)
        {
            var _container = await _context.AddContainer(container);
            return CreatedAtAction(nameof(GetContainer), new { id = container.Id }, container);
        }

        [HttpPost("{row}/{col}/{shelf}")]
        public async Task<ActionResult<Container>> GenerateWarehouse(int row, int col, int shelf)
        {
            Container container = new Container();
            container.containerRow= row;
            container.containerColumn = col;
            container.containerNumber = shelf;
            var _container = await _context.AddContainer(container);
            return CreatedAtAction(nameof(GetContainer), new { id = container.Id }, container);
        }

        // DELETE: api/Containers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContainer(int id)
        {
            var _container = await _context.DeleteContainer(id);
            return _container;
        }

        
    }
}
