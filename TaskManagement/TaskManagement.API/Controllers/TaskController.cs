using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.BLL.Interfaces;
using TaskManagement.Shared.DTOs;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public TaskController(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("usernames")]
        public async Task<IActionResult> GetAllUsernames()
        {
            var usernames = await _userService.GetAllUsernames();
            if (usernames == null ||  usernames.Count == 0)
            {
                return Ok(new List<int>());
            }

            return Ok(usernames);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized("User ID claim is missing or invalid.");
            }

            var id = await _taskService.CreateTaskAsync(dto, userId);
            return Ok(new { id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("admin")]
        public async Task<IActionResult> CreateAdminTask([FromBody] CreateAdminTaskDto dto)
        {
            var id = await _taskService.CreateAdminTaskAsync(dto);
            return Ok(new { id });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public async Task<IActionResult> GetAllAdminTasks()
        {
            var tasks = await _taskService.GetAllAdminTasks();
            return Ok(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized("User ID claim is missing or invalid.");
            }

            var tasks = await _taskService.GetAllAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
        {
            var success = await _taskService.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return Ok(success);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] UpdateTaskStatusDto dto)
        {
            var success = await _taskService.UpdateStatusAsync(id, dto);
            if (!success) return NotFound();
            return Ok(success);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok(success);
        }
    }
}