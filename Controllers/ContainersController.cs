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
    public class ContainersController : ControllerBase
    {
        private readonly SolarSystemsDbContext _context;

        public ContainersController(SolarSystemsDbContext context)
        {
            _context = context;
        }

        // GET: api/Containers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Container>>> GetContainer()
        {
          if (_context.Container == null)
          {
              return NotFound();
          }
            return await _context.Container.ToListAsync();
        }

        // GET: api/Containers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Container>> GetContainer(int id)
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

        // GET by row and column and number
        [HttpGet("containernumber/{containerRow}/{containerColumn}/{containerNumber}")]
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

        // PUT: api/Containers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContainer(int id, Container container)
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

        // POST: api/Containers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Container>> PostContainer(Container container)
        {
          if (_context.Container == null)
          {
              return Problem("Entity set 'SolarSystemsDbContext.Container'  is null.");
          }
            _context.Container.Add(container);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContainer", new { id = container.Id }, container);
        }

        // DELETE: api/Containers/5
        [HttpDelete("{id}")]
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

        private bool ContainerExists(int id)
        {
            return (_context.Container?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
