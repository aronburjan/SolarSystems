using Microsoft.AspNetCore.Mvc;
using SolarSystems.Models;

namespace SolarSystems.Services
{
    public interface IProjectComponentService
    {
        public Task AddProjectComponent(ProjectComponent projectComponent);

        public Task<ActionResult<IEnumerable<ProjectComponent>>> GetProjectComponents();
    }
}
