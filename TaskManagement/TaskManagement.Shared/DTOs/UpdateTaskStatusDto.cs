using TaskStatus = TaskManagement.Shared.Enums.TaskStatus;

namespace TaskManagement.Shared.DTOs
{
    public class UpdateTaskStatusDto
    {
        public TaskStatus Status { get; set; }
    }
}
