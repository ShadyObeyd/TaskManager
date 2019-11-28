namespace TaskManager.Services
{
    using Data;
    using Models.ViewModels.Comments;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class CommentService
    {
        private readonly TaskManagerContext db;
        public CommentService(TaskManagerContext db)
        {
            this.db = db;
        }

        public List<CommentViewModel> GetCommentViewModel(string taskId)
        {
            return this.db.Comments.Where(c => c.TaskId == taskId).Select(c => new CommentViewModel
            {
                Id = c.Id,
                Content = c.Content,
                Creator = c.Creator.UserName,
                DateAdded = c.DateAdded.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)
            }).ToList();
        }
    }
}