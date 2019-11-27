namespace TaskManager.Models.DataModels
{
    using System;
    using System.Collections.Generic;

    public class Task : BaseModel
    {
        public Task()
        {
            this.CreatedDate = DateTime.Now;
            this.Status = new HashSet<TaskStatus>();
            this.Type = new HashSet<TaskType>();
            this.AssignedTo = new HashSet<UserTask>();
            this.Comments = new HashSet<Comment>();
        }
        public DateTime CreatedDate { get; set; }
        public DateTime RequiredBy { get; set; }

        public string TaskDescription { get; set; }

        public virtual ICollection<TaskStatus> Status { get; set; }

        public virtual ICollection<TaskType> Type { get; set; }

        public virtual ICollection<UserTask> AssignedTo { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public string Creator { get; set; }
    }
}
