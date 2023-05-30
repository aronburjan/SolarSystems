using Microsoft.AspNetCore.Mvc;
using SolarSystems.Models;

namespace SolarSystems.Services
{
    public interface IContainerService
    {
        public Task<ActionResult<IEnumerable<Container>>> GetContainer();
        public  Task<ActionResult<Container>> GetContainerById(int id);
        public Task<ActionResult<Container>> GetContainerNumber(int containerRow, int containerColumn, int containerNumber);

        public  Task<IActionResult> UpdateContainer(int id, Container container);

        public Task<ActionResult<IEnumerable<Component>>> ListAvailableComponents();

        public Task removeComponentsFromContaienr(int componentId, int quantity);

        public Task<ActionResult<IEnumerable<Container>>> GetContainersByComponentId(int componentId);

        public int NumberOfAvailableComponentsById(int id);
        public Task<ActionResult<Container>> AddContainer(Container container);

        public  Task<IActionResult> DeleteContainer(int id);

        public bool ContainerExists(int id);


    }
}
