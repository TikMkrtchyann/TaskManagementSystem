using TaskStatus = TaskManagement.Shared.Enums.TaskStatus;

namespace TaskManagement.DAL.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
    }
}
