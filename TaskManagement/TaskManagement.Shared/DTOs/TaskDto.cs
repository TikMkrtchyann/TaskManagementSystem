using TaskStatus = TaskManagement.Shared.Enums.TaskStatus;


namespace TaskManagement.Shared.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
        public int UserId { get; set; }
    }

}
