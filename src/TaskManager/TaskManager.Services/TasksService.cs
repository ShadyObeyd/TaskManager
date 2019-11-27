namespace TaskManager.Services
{
    using Data;
    using Models.DataModels;
    using Models.ViewModels.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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

        public void CreateTask(CreateTaskViewModel inputModel, string userId)
        {
            var task = new Task
            {
                CreatedDate = DateTime.Now,
                Creator = this.db.Users.FirstOrDefault(u => u.Id == userId).UserName,
                TaskDescription = inputModel.TaskDescription,
                RequiredBy = DateTime.ParseExact(inputModel.RequiredByDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Status = GenerateStatuses(inputModel.TaskStatus),
                Type = GenerateTypes(inputModel.TaskType),
            };

            var usersTasks = GetAssignedTo(inputModel.AssignedTo, task);

            this.db.Tasks.Add(task);
            this.db.UsersTasks.AddRange(usersTasks);
            this.db.SaveChanges();
        }

        private HashSet<UserTask> GetAssignedTo(string usernames, Task task)
        {
            var usersTasksCollection = new HashSet<UserTask>();
            var usernameTokens = usernames.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            foreach (var usernameToken in usernameTokens)
            {
                var userTask = new UserTask
                {
                    Task = task,
                    UserId = this.db.Users.FirstOrDefault(u => u.UserName == usernameToken).Id
                };

                usersTasksCollection.Add(userTask);
            }

            return usersTasksCollection;
        }

        private HashSet<TaskType> GenerateTypes(string types)
        {
            var typeCollection = new HashSet<TaskType>();

            var typeTokens = types.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            foreach (var typeToken in typeTokens)
            {
                var type = new TaskType
                {
                    Content = typeToken
                };

                typeCollection.Add(type);
            }

            return typeCollection;
        }
        private HashSet<TaskStatus> GenerateStatuses(string statuses)
        {
            var statusCollection = new HashSet<TaskStatus>();

            var statusTokens = statuses.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            foreach (var statusToken in statusTokens)
            {
                var status = new TaskStatus
                {
                    Content = statusToken
                };

                statusCollection.Add(status);
            }

            return statusCollection;
        }
    }
}