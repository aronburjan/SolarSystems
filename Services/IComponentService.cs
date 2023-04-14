using Microsoft.AspNetCore.Mvc;
using SolarSystems.Models;
namespace SolarSystems.Services
{
    public interface IComponentService
    {
        public  Task<ActionResult<IEnumerable<Component>>> GetComponent(); //itt nem lehet ascnc
        public  Task<ActionResult<Component>> GetComponentById(int id);

        public  Task<IActionResult> UpdateComponent(int id, Component component);

        public Task<IActionResult> PutComponentNewPrice(int id, int price);

        public Task<ActionResult<Component>> AddComponent(Component component);
        public bool ComponentExists(int id);

        public Task<ActionResult<Component>> AddComponentToContainer(string componentName, int containerRow, int containerCol, int containerNumber, int quantity);

        public Task<ActionResult<Component>> GetComponentByName(string componentName);
        public Task<IActionResult> DeleteComponent(int id);

    }
}
