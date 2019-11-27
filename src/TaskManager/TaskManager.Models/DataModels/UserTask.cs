namespace TaskManager.Models.DataModels
{
    public class UserTask
    {
        public string TaskId { get; set; }

        public virtual Task Task { get; set; }

        public string UserId { get; set; }

        public virtual TaskManagerUser User { get; set; }
    }
}
