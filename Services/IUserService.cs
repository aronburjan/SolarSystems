using Microsoft.AspNetCore.Mvc;
using SolarSystems.Models;

namespace SolarSystems.Services
{
    public interface IUserService
    {
        public Task<ActionResult<IEnumerable<User>>> GetUsers();
        public Task<ActionResult<User>> GetUserById(int id);
        public Task<ActionResult<IEnumerable<User>>> GetUserByUsernameAndPassword(string username, string password);
        public Task<ActionResult<User>> AddUser(User user);
        public Task<IActionResult> UpdateUser(int id, User user);
        public Task<IActionResult> DeleteUser(int id);

        public bool UserExists(long id);
    }


}
