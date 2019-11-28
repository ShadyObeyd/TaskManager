namespace TaskManager.Services
{
    using Data;
    using Models.ViewModels.Comments;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Models.DataModels;
    using System;

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
        
        public void CreateComment(string taskId, string content, string creatorId)
        {
            var comment = new Comment
            {
                TaskId = taskId,
                Content = content,
                DateAdded = DateTime.Now,
                CreatorId = creatorId
            };

            this.db.Comments.Add(comment);
            this.db.SaveChanges();
        }

        public void DeleteComment(string commentId)
        {
            var comment = this.db.Comments.FirstOrDefault(c => c.Id == commentId);

            if (comment == null)
            {
                throw new ArgumentException();
            }

            this.db.Comments.Remove(comment);
            this.db.SaveChanges();
        }
    }
}