using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.BLL.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("usernames")]
        public async Task<IActionResult> GetAllUsernames()
        {
            var usernames = await _userService.GetAllUsernames();
            if (usernames == null || usernames.Count == 0)
            {
                return Ok(new List<int>());
            }

            return Ok(usernames);
        }

        [HttpGet("userIdList")]
        public async Task<IActionResult> GetAllUserId()
        {
            var userIds = await _userService.GetAllUserId();
            if (userIds == null || userIds.Count == 0)
            {
                return Ok(new List<int>());
            }

            return Ok(userIds);
        }
    }
}
