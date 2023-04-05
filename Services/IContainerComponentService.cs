using Microsoft.AspNetCore.Mvc;

namespace SolarSystems.Services
{
    public interface IContainerComponentService
    {
        public Task<ActionResult<Models.Component>> addComponentToContainer(Models.Component component, int containerRow, int containerCol, int containerNumber);
    }
}
