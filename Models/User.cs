using Microsoft.AspNetCore.Identity;

namespace SolarSystems.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Password { get; set; }
        public int accessLevel { get; set; }

        public ICollection<Project>? Projects { get; set; }
    }
}
