namespace TaskManager.App.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using TaskManager.Models.DataModels;

    public class CommentsController : Controller
    {
        private readonly CommentService commentService;
        private readonly UserManager<TaskManagerUser> userManager;
        public CommentsController(CommentService commentService, UserManager<TaskManagerUser> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult Create(string taskId, string content)
        {
            if (string.IsNullOrEmpty(taskId) || string.IsNullOrEmpty(content))
            {
                return this.RedirectToAction("Index", "Home");
            }

            var userId = this.userManager.GetUserId(HttpContext.User);
            this.commentService.CreateComment(taskId, content, userId);

            return this.RedirectToAction("Open", "Tasks", new { taskId });
        }

        public IActionResult Delete(string commentId, string taskId)
        {
            if (string.IsNullOrEmpty(commentId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.commentService.DeleteComment(commentId);

            return this.RedirectToAction("Open", "Tasks", new { taskId });
        }
    }
}