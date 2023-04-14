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

        public async Task<ActionResult<Component>> GetComponentByName(string componentName)
        {
            if (_context.Component == null)
            {
                return NotFound();
            }
            var component = await _context.Component.Where(c => c.componentName.Equals(componentName)).FirstOrDefaultAsync();

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


        public async Task<ActionResult<Component>> AddComponentToContainer(string componentName, int containerRow, int containerCol, int containerNumber, int quantity) {
            if (_context.Component == null)
            {
                return Problem("Entity set 'SolarSystemsDbContext.Component'  is null.");
            }
            ContainerService containerService = new ContainerService(_context);
            ComponentService componentService = new ComponentService(_context);
            var container = await containerService.GetContainerNumber(containerRow, containerCol, containerNumber);
            var component = await componentService.GetComponentByName(componentName);
            if(component == null)
            {
                return Problem("Invalid component name");
            }
            if(container.Value == null)
            {
                return Problem("Invalid container.");
            }
            if((container.Value.freeSpace - component.Value.maxStack*quantity) < 0)
            {
                return Problem("Component will not fit into container.");
            }
            else
            {
                container.Value.Component = component.Value;
                container.Value.freeSpace -= component.Value.maxStack*quantity;
                container.Value.quantityInContainer += quantity;
                await containerService.UpdateContainer(container.Value.Id, container.Value);
            }
            return NoContent();
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
