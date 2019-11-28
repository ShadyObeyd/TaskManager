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
        private readonly CommentService commentService;
        public TasksService(TaskManagerContext db, CommentService commentService)
        {
            this.db = db;
            this.commentService = commentService;
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
                Status = this.GenerateStatuses(inputModel.TaskStatus),
                Type = this.GenerateTypes(inputModel.TaskType)
            };

            var usersTasks = GetAssignedTo(inputModel.AssignedTo, task);

            this.db.Tasks.Add(task);
            this.db.UsersTasks.AddRange(usersTasks);
            this.db.SaveChanges();
        }

        public ReadTaskViewModel GetReadTaskModel(string taskId)
        {
            var task = this.db.Tasks.FirstOrDefault(t => t.Id == taskId);

            if (task == null)
            {
                throw new ArgumentException();
            }

            var assginedUsers = this.db.UsersTasks.Where(ut => ut.TaskId == taskId).Select(ut => ut.User.UserName);
            var statuses = this.db.TaskStatuses.Where(ts => ts.TaskId == taskId).Select(ts => ts.Content);
            var types = this.db.TaskTypes.Where(tt => tt.TaskId == taskId).Select(tt => tt.Content);

            return new ReadTaskViewModel
            {
                Id = task.Id,
                Creator = task.Creator,
                Content = task.TaskDescription,
                Comments = this.commentService.GetCommentViewModel(taskId),
                DateCreated = task.CreatedDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequiredByDate = task.RequiredBy.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                AssignedTo = string.Join(", ", assginedUsers),
                Statuses = string.Join(", ", statuses),
                Types = string.Join(", ", types)
            };
        }

        public void DeleteTask(string taskId)
        {
            var taskStatuses = this.db.TaskStatuses.Where(t => t.TaskId == taskId);
            var taskTypes = this.db.TaskTypes.Where(t => t.TaskId == taskId);
            var assignedToUsers = this.db.UsersTasks.Where(t => t.TaskId == taskId);
            var comments = this.db.Comments.Where(c => c.TaskId == taskId);
            var tasks = this.db.Tasks.FirstOrDefault(t => t.Id == taskId);

            this.db.TaskStatuses.RemoveRange(taskStatuses);
            this.db.TaskTypes.RemoveRange(taskTypes);
            this.db.UsersTasks.RemoveRange(assignedToUsers);
            this.db.Comments.RemoveRange(comments);
            this.db.Tasks.Remove(tasks);

            this.db.SaveChanges();
        }

        public EditTaskViewModel GetEditModel(string taskId)
        {
            var task = this.db.Tasks.FirstOrDefault(t => t.Id == taskId);

            var assginedUsers = this.db.UsersTasks.Where(ut => ut.TaskId == taskId).Select(ut => ut.User.UserName);
            var statuses = this.db.TaskStatuses.Where(ts => ts.TaskId == taskId).Select(ts => ts.Content);
            var types = this.db.TaskTypes.Where(tt => tt.TaskId == taskId).Select(tt => tt.Content);

            return new EditTaskViewModel
            {
                Id = task.Id,
                Creator = task.Creator,
                Content = task.TaskDescription,
                DateCreated = task.CreatedDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                RequiredByDate = task.RequiredBy.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                AssignedTo = string.Join(", ", assginedUsers),
                Statuses = string.Join(", ", statuses),
                Types = string.Join(", ", types)
            };
        }

        public void EditTask(string taskId, EditTaskViewModel inputModel)
        {
            var task = this.db.Tasks.FirstOrDefault(t => t.Id == taskId);

            task.TaskDescription = inputModel.Content;
            task.RequiredBy = DateTime.ParseExact(inputModel.RequiredByDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var statuses = this.db.TaskStatuses.Where(ts => ts.TaskId == taskId);
            var types = this.db.TaskTypes.Where(tt => tt.TaskId == taskId);

            this.db.RemoveRange(statuses);
            this.db.RemoveRange(types);

            task.Status = this.GenerateStatuses(inputModel.Statuses);
            task.Type = this.GenerateTypes(inputModel.Types);

            var usersTasks = this.db.UsersTasks.Where(t => t.TaskId == taskId);

            this.db.RemoveRange(usersTasks);

            var newUsersTasks = this.GetAssignedTo(inputModel.AssignedTo, task);

            this.db.UsersTasks.AddRange(newUsersTasks);

            this.db.Tasks.Update(task);
            this.db.SaveChanges();
        }

        private HashSet<UserTask> GetAssignedTo(string usernames, Task task)
        {
            var usersTasksCollection = new HashSet<UserTask>();
            var usernameTokens = usernames.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            foreach (var usernameToken in usernameTokens)
            {
                if (!this.db.Users.Any(u => u.UserName == usernameToken))
                {
                    continue;
                }

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