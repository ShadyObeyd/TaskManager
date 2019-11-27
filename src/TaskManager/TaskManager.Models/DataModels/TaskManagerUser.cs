namespace TaskManager.Models.DataModels
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class TaskManagerUser : IdentityUser
    {
        public TaskManagerUser()
        {
            this.Tasks = new HashSet<UserTask>();
            this.WrittenComments = new HashSet<Comment>();
        }
        public virtual ICollection<UserTask> Tasks { get; set; }

        public virtual ICollection<Comment> WrittenComments { get; set; }
    }
}