using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarSystems.Models;

namespace SolarSystems.Services
{
    public class ContainerComponentService : IContainerComponentService
    {
        private readonly SolarSystemsDbContext _context;
        public ContainerComponentService(SolarSystemsDbContext context)
        {
            _context = context;
        }

        public Task<ActionResult<Component>> addComponentToContainer(Component component, int containerRow, int containerCol, int containerNumber)
        {
            throw new NotImplementedException();
        }
    }
}
