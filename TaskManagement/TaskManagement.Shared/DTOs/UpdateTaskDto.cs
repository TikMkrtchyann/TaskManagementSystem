using TaskStatus = TaskManagement.Shared.Enums.TaskStatus;

namespace TaskManagement.Shared.DTOs
{
    public class UpdateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
    }
}
