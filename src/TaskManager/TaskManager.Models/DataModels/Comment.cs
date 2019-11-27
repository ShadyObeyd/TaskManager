namespace TaskManager.Models.DataModels
{
    using System;
    using System.Collections.Generic;

    public class Comment : BaseModel
    {
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public string CreatorId { get; set; }
        public virtual TaskManagerUser Creator { get; set; }
    }
}