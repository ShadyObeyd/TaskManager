namespace TaskManager.App.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using TaskManager.App.Models;
    using Services;

    public class HomeController : Controller
    {
        private readonly TasksService tasksService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, TasksService tasksService)
        {
            _logger = logger;
            this.tasksService = tasksService;
        }

        public IActionResult Index()
        {
            var viewModel = this.tasksService.GetAllTasks();

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}