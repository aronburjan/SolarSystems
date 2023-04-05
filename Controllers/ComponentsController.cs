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
    public class ComponentsController : ControllerBase
    {
        private readonly SolarSystemsDbContext _context;

        public ComponentsController(SolarSystemsDbContext context)
        {
            _context = context;
        }

        // GET: api/Components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component>>> GetComponent()
        {
          if (_context.Component == null)
          {
              return NotFound();
          }
            return await _context.Component.ToListAsync();
        }

        // GET: api/Components/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetComponent(int id)
        {
          if (_context.Component == null)
          {
              return NotFound();
          }
            var component = await _context.Component.FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            return component;
        }

        // PUT: api/Components/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(int id, Component component)
        {
            if (id != component.Id)
            {
                return BadRequest();
            }

            _context.Entry(component).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(id))
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

        // PUT: api/Components/5/5000
        [HttpPut("{id}/{price}")]
        public async Task<IActionResult> PutComponentNewPrice(int id, int price)
        {

            var component = await _context.Component.FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }
            component.price = price;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(id))
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

        // POST: api/Components
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Component>> PostComponent(Component component)
        {
          if (_context.Component == null)
          {
              return Problem("Entity set 'SolarSystemsDbContext.Component'  is null.");
          }
            _context.Component.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponent", new { id = component.Id }, component);
        }

        //SERVICE
        [HttpPost("{containerRow}/{containerCol}/{containerNumber}")]
        public async Task<ActionResult<Component>> AddComponentToContainer(Component component, int containerRow, int containerCol, int containerNumber)
        {
            if (_context.Component == null)
            {
                return Problem("Entity set 'SolarSystemsDbContext.Component'  is null.");
            }

            var container = await _context.Container.Where(c => c.containerRow == containerRow && c.containerColumn == containerCol && c.containerNumber == containerNumber).FirstOrDefaultAsync();
            if(container == null)
            {
                return Problem("Invalid container!");
            }
            else
            {
                _context.Component.Add(component);
                await _context.SaveChangesAsync();
                container.Component = component;
            }

            
            

            return CreatedAtAction("GetComponent", new { id = component.Id }, component);
        }


        // DELETE: api/Components/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            if (_context.Component == null)
            {
                return NotFound();
            }
            var component = await _context.Component.FindAsync(id);
            if (component == null)
            {
                return NotFound();
            }

            _context.Component.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComponentExists(int id)
        {
            return (_context.Component?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
