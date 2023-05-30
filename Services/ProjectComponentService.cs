using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SolarSystems.Models;

namespace SolarSystems.Services
{
    public class ProjectComponentService : IProjectComponentService
    {

        private readonly SolarSystemsDbContext _context;

        public ProjectComponentService(SolarSystemsDbContext context)
        {
            _context = context;
        }
        public async Task AddProjectComponent(ProjectComponent projectComponent)
        {
            _context.ProjectComponent.Add(projectComponent);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable< ProjectComponent>>> GetProjectComponent(int id)
        {
            var projectComponents = await GetProjectComponents();
            List<ProjectComponent> projectComponentsOfProject = new List<ProjectComponent>();
            foreach (var projectComponent in projectComponents.Value)
            {
                if(projectComponent.ProjectId == id)
                {
                    projectComponentsOfProject.Add(projectComponent);
                }
            }
            return projectComponentsOfProject;
        }

        public async Task<ActionResult<IEnumerable<ProjectComponent>>> GetProjectComponents()
        {
            var projectComponents = await _context.ProjectComponent.ToListAsync();
            return projectComponents;
        }
    }
}
