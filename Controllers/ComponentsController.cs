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
    public class ComponentsController : ControllerBase
    {
        private readonly IComponentService _context; //ehelyett iuserservice

        public ComponentsController(IComponentService context)
        {
            _context = context;
        }

        // GET: api/Components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component>>> GetComponent()
        {
            return await _context.GetComponent();
        }

        // GET: api/Components/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetComponent(int id)
        {     
            var component = await _context.GetComponentById(id);

            return component;
        }

        // PUT: api/Components/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(int id, Component component)
        {
            var _component = await _context.UpdateComponent(id, component);
            return _component;

        }

        // PUT: api/Components/5/5000
        [HttpPut("{id}/{price}")]
        public async Task<IActionResult> PutComponentNewPrice(int id, int price)
        {

            var component = await _context.PutComponentNewPrice(id, price);
            return component;

            
        }

        // POST: api/Components
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Component>> PostComponent(Component component)
        {
            var _component = await _context.AddComponent(component);
            return CreatedAtAction(nameof(GetComponent), new { id = component.Id },component);
        }

        //SERVICE
        [HttpPost("{componentName}/{containerRow}/{containerCol}/{containerNumber}/{orderQuantity}")]
        public async Task<ActionResult<Component>> AddComponentToContainer(string componentName, int containerRow, int containerCol, int containerNumber, int orderQuantity)
        {
            var _component = await _context.AddComponentToContainer(componentName, containerRow,containerCol,containerNumber, orderQuantity);
            return NoContent();
        }


        // DELETE: api/Components/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            var _component = await _context.DeleteComponent(id);
            return _component;
        }
            


    }
}
