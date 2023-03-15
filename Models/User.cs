using Microsoft.AspNetCore.Identity;

namespace SolarSystems.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        public string? Password { get; set; }
        public int accessLevel { get; set; }

        public User(long id, string name, string password, int accessLevel)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
            this.accessLevel = accessLevel;
        }

        public User() { }
    }
}
