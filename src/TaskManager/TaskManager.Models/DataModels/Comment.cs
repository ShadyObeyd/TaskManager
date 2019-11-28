namespace TaskManager.Models.DataModels
{
    using System;

    public class Comment : BaseModel
    {
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public string CreatorId { get; set; }
        public virtual TaskManagerUser Creator { get; set; }

        public string TaskId { get; set; }

        public virtual Task Task { get; set; }
    }
}