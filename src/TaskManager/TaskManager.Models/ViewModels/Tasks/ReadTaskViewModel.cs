namespace TaskManager.Models.ViewModels.Tasks
{
    using System.Collections.Generic;
    using Comments;
    public class ReadTaskViewModel
    {
        public string Id { get; set; }
        public string Creator { get; set; }

        public string Content { get; set; }

        public string DateCreated { get; set; }

        public string RequiredByDate { get; set; }

        public string Statuses { get; set; }

        public string Types { get; set; }

        public string AssginedTo { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}