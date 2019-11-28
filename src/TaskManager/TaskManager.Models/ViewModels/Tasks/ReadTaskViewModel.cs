namespace TaskManager.Models.ViewModels.Tasks
{
    using System.Collections.Generic;
    using Comments;

    public class ReadTaskViewModel : EditTaskViewModel
    {
        public ICollection<CommentViewModel> Comments { get; set; }
    }
}