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

            public async Task<ActionResult<IEnumerable<Container>>> GetContainer() {
                if (_context.Container == null)
                {
                    return NotFound();
                }
                return await _context.Container.ToListAsync();
            }

            public async Task<ActionResult<Container>> GetContainerById(int id) {
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

            public async Task<ActionResult<IEnumerable<Container>>> GetContainerNumber(int containerRow, int containerColumn, int containerNumber)
            {
                if (_context.Container == null)
                {
                    return NotFound();
                }
                var container = await _context.Container.Where(a => a.containerRow == containerRow
                                                          && a.containerColumn == containerRow && a.containerNumber == containerNumber).ToListAsync();

                if (container == null)
                {
                    return NotFound();
                }

                return container;
            }

            public async Task<IActionResult> UpdateContainer(int id, Container container) {
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
            public async Task<ActionResult<Container>> AddContainer(Container container) {
                if (_context.Container == null)
                {
                    return Problem("Entity set 'SolarSystemsDbContext.Container'  is null.");
                }
                _context.Container.Add(container);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetContainerById), new { id = container.Id }, container);
            }

            public async Task<IActionResult> DeleteContainer(int id) {
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

    }
}
