using Microsoft.AspNetCore.Identity;

namespace SolarSystems.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Password { get; set; }
        public int accessLevel { get; set; }

        public virtual Project Project { get; set; }

        public static implicit operator User(HashSet<User> v)
        {
            throw new NotImplementedException();
        }
    }
}
