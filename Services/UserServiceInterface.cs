using Microsoft.AspNetCore.Mvc;
using SolarSystems.Models;

namespace SolarSystems.Services
{
    public interface UserServiceInterface
    {
        Task<ActionResult<User>> GetUser(long id);

        Task<ActionResult<IEnumerable<User>>> GetUsers();

        Task<ActionResult<User>> PostUser(User user);

        Task<IActionResult> PutUser(long id, User user);
    }
}
