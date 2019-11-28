﻿namespace TaskManager.Models.DataModels
{
    public class TaskType : BaseModel
    {
        public string Content { get; set; }

        public string TaskId { get; set; }

        public virtual Task Task { get; set; }
    }
}