using Microsoft.AspNetCore.Mvc;
using SolarSystems.Models;
namespace SolarSystems.Services
{
    public interface IProjectService
    {
        public Task<ActionResult<IEnumerable<Project>>> GetProject();

        public  Task<ActionResult<Project>> GetProjectById(int id);

        public  Task<IActionResult> UpdateProject(int id, Project project);

        public  Task<ActionResult<Project>> AddProject(Project project);

        public  Task<IActionResult> DeleteProject(int id);

        public bool ProjectExists(int id);
    }
}
