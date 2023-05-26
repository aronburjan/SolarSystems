using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarSystems.Models;

namespace SolarSystems.Services
{
    public class ContainerService : ControllerBase, IContainerService
    {
        private readonly SolarSystemsDbContext _context;

        public ContainerService(SolarSystemsDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Container>>> GetContainer()
        {
            if (_context.Container == null)
            {
                return NotFound();
            }
            return await _context.Container.ToListAsync();
        }

        public async Task<ActionResult<Container>> GetContainerById(int id)
        {
            if (_context.Container == null)
            {
                return NotFound();
            }
            var container = await _context.Container.FindAsync(id);

            if (container == null)
            {
                return NotFound();
            }

            return container;
        }

        public async Task<ActionResult<Container>> GetContainerNumber(int containerRow, int containerColumn, int containerNumber)
        {
            if (_context.Container == null)
            {
                return NotFound();
            }
            var container = await _context.Container.Where(a => a.containerRow == containerRow
                                                      && a.containerColumn == containerColumn && a.containerNumber == containerNumber).FirstOrDefaultAsync();

            if (container == null)
            {
                return NotFound();
            }

            return container;
        }

        public async Task<IActionResult> UpdateContainer(int id, Container container)
        {
            if (id != container.Id)
            {
                return BadRequest();
            }

            _context.Entry(container).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContainerExists(id))
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
        public async Task<ActionResult<Container>> AddContainer(Container container)
        {
            if (_context.Container == null)
            {
                return Problem("Entity set 'SolarSystemsDbContext.Container'  is null.");
            }
            _context.Container.Add(container);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContainerById), new { id = container.Id }, container);
        }

        public async Task<IActionResult> DeleteContainer(int id)
        {
            if (_context.Container == null)
            {
                return NotFound();
            }
            var container = await _context.Container.FindAsync(id);
            if (container == null)
            {
                return NotFound();
            }

            _context.Container.Remove(container);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public bool ContainerExists(int id)
        {
            return (_context.Container?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<ActionResult<IEnumerable<Component>>> ListAvailableComponents()
        {
            List<Component> availableComponentsList = new List<Component>();
            List<string> availableComponentNames = new List<string>();
            var availableComponents = await _context.Container.Include(c => c.Component) // Explicitly include the Component objects
                                                              .Where(c => c.Component != null)
                                                              .ToListAsync();
            if (availableComponents.Any())
            {
                for (int i = 0; i < availableComponents.Count; i++)
                {
                    availableComponentsList.Add(availableComponents[i].Component);
                    Console.WriteLine(availableComponents[i].Component.componentName);
                }
                return availableComponentsList;
            }
            else
            {
                return NotFound();
            }

        }

        public int NumberOfAvailableComponentsById(int id)
        {
            var containersHoldingComponent = _context.Container.Include(c => c.Component) // Explicitly include the Component objects
                                                              .Where(c => c.Component.Id == id)
                                                              .ToListAsync();
            int availableQuantity = 0;
            for (int i = 0; i < containersHoldingComponent.Result.Count; i++)
            {
                availableQuantity += containersHoldingComponent.Result[i].quantityInContainer;
            }
            return availableQuantity;
        }

        public async Task removeComponentsFromContaienr(int componentId, int quantity)
        {
            //componentId: the component you want to remove
            //quantity: the number of components you want to remove. If it reaches 0, the item container should become empty
            int removedSoFar = 0;
            //get a list of containers containing the component with componentId
            //we remove components until we reach the end of the list of non-empty containers or if we removed the desired amount
            var containersHoldingComponent = await _context.Container.Include(c => c.Component) // Explicitly include the Component objects
                                                             .Where(c => c.Component.Id == componentId)
                                                             .ToListAsync();
            for (int i = 0; i < containersHoldingComponent.Count; i++)
            {
                if(quantity == 0)
                {
                    await _context.SaveChangesAsync();
                    return;
                }
                if (containersHoldingComponent[i].quantityInContainer - quantity <= 0)
                {
                    removedSoFar = containersHoldingComponent[i].quantityInContainer;
                    containersHoldingComponent[i].quantityInContainer = 0;
                    containersHoldingComponent[i].Component = null;
                    containersHoldingComponent[i].ComponentId = null;
                    await _context.SaveChangesAsync();
                    quantity -= removedSoFar;
                }
                else
                {
                    containersHoldingComponent[i].quantityInContainer -= quantity;
                    await _context.SaveChangesAsync();
                    return;
                }
            }
            await _context.SaveChangesAsync();
            return;
        }

    }
}