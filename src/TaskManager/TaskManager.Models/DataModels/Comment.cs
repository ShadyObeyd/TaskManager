namespace TaskManager.Models.DataModels
{
    using System;
    using System.Collections.Generic;

    public class Comment : BaseModel
    {
        public Comment()
        {
            this.DateAdded = DateTime.Now;
            this.Type = new HashSet<CommentType>();
        }

        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public virtual ICollection<CommentType> Type { get; set; }

        public DateTime ReminderDate { get; set; }

        public string CreatorId { get; set; }
        public virtual TaskManagerUser Creator { get; set; }
    }
}