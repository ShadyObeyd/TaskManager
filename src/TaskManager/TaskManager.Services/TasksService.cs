namespace TaskManager.Services
{
    using Data;
    using Models.ViewModels.Tasks;
    using System.Linq;

    public class TasksService
    {
        private readonly TaskManagerContext db;
        public TasksService(TaskManagerContext db)
        {
            this.db = db;
        }

        public IndexTaskListViewModel GetAllTasks()
        {
            var model = new IndexTaskListViewModel();

            var tasks = this.db.Tasks.Select(t => new IndexTaskViewModel
            {
                Id = t.Id,
                Content = t.TaskDescription
            });

            foreach (var task in tasks)
            {
                model.Tasks.Add(task);
            }

            return model;
        }
    }
}