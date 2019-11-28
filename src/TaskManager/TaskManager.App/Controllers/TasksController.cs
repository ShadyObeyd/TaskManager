namespace TaskManager.App.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using TaskManager.Models.DataModels;
    using TaskManager.Models.ViewModels.Tasks;
    using TaskManager.Services;

    public class TasksController : Controller
    {
        private readonly TasksService tasksService;
        private readonly UserManager<TaskManagerUser> userManager;
        public TasksController(TasksService tasksService, UserManager<TaskManagerUser> userManager)
        {
            this.tasksService = tasksService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateTaskViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.userManager.GetUserId(HttpContext.User);

            this.tasksService.CreateTask(inputModel, userId);

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Open(string taskId)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                return this.RedirectToAction("Index", "Home");
            }

            try
            {
                var model = this.tasksService.GetReadTaskModel(taskId);

                return this.View(model);
            }
            catch (ArgumentException)
            {
                return this.RedirectToAction("Home", "Index");
            }
        }
    }
}