using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarSystems.Models;

namespace SolarSystems.Services
{
    public class ComponentService :  ControllerBase, IComponentService
    {
        private readonly SolarSystemsDbContext _context;

        public ComponentService(SolarSystemsDbContext context) { 
            
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Component>>> GetComponent() //itt meg kell az async
        {
            if (_context.Component == null)
            {
                return NotFound();
            }
            return await _context.Component.ToListAsync();
        }

        public async Task<ActionResult<Component>> GetComponentById(int id)
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

        public async Task<IActionResult> UpdateComponent(int id, Component component) {
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

        public async Task<IActionResult> PutComponentNewPrice(int id, int price) {
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
        public async Task<ActionResult<Component>> AddComponent(Component component) {
            if (_context.Component == null)
            {
                return Problem("Entity set 'SolarSystemsDbContext.Component'  is null.");
            }
            _context.Component.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComponentById), new { id = component.Id }, component);
        }


        public async Task<ActionResult<Component>> AddComponentToContainer(Component component, int containerRow, int containerCol, int containerNumber) {
            if (_context.Component == null)
            {
                return Problem("Entity set 'SolarSystemsDbContext.Component'  is null.");
            }

            var container = await _context.Container.Where(c => c.containerRow == containerRow && c.containerColumn == containerCol && c.containerNumber == containerNumber).FirstOrDefaultAsync();
            if (container == null)
            {
                return Problem("Invalid container!");
            }
            else
            {
                _context.Component.Add(component);
                await _context.SaveChangesAsync();
                container.Component = component;
            }

            return CreatedAtAction(nameof(GetComponentById), new { id = component.Id }, component);
        }

        public async Task<IActionResult> DeleteComponent(int id) {
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


        public bool ComponentExists(int id) {
            return (_context.Component?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
