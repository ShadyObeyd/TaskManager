namespace TaskManager.App.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System;
    using TaskManager.Models.DataModels;

    public class CommentsController : Controller
    {
        private readonly CommentService commentService;
        private readonly UserManager<TaskManagerUser> userManager;
        private readonly TasksService tasksService;

        public CommentsController(CommentService commentService, UserManager<TaskManagerUser> userManager, TasksService tasksService)
        {
            this.commentService = commentService;
            this.userManager = userManager;
            this.tasksService = tasksService;
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

            try
            {
                this.commentService.DeleteComment(commentId);

                return this.RedirectToAction("Open", "Tasks", new { taskId });
            }
            catch (ArgumentException)
            {
                return this.RedirectToAction("Open", "Tasks", new { taskId });
            }

        }

        [HttpGet]
        public IActionResult Edit(string taskId)
        {
            var model = this.tasksService.GetReadTaskModel(taskId);
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(string taskId, string commentId, string content)
        {
            try
            {
                this.commentService.EditComment(commentId, content);

                return this.RedirectToAction("Open", "Tasks", new { taskId });
            }
            catch (ArgumentException)
            {
                var model = this.tasksService.GetReadTaskModel(taskId);
                return this.View(model);
            }
        }
    }
}