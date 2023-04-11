using Microsoft.AspNetCore.Mvc;
using SolarSystems.Models;

namespace SolarSystems.Services
{
    public interface IProjectStatusService
    {
        public Task<ActionResult<IEnumerable<ProjectStatus>>> GetProjectStatus();

        public  Task<ActionResult<ProjectStatus>> GetProjectStatusById(int id);

        public Task<IActionResult> UpdateProjectStatus(int id, ProjectStatus projectStatus);

        public Task<ActionResult<ProjectStatus>> AddProjectStatus(ProjectStatus projectStatus);

        public  Task<IActionResult> DeleteProjectStatus(int id);

        public bool ProjectStatusExists(int id);
    }
}
