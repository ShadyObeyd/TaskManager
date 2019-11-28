namespace TaskManager.Models.ViewModels.Tasks
{
    public class EditTaskViewModel
    {
        public string Id { get; set; }
        public string Creator { get; set; }

        public string Content { get; set; }

        public string DateCreated { get; set; }

        public string RequiredByDate { get; set; }

        public string Statuses { get; set; }

        public string Types { get; set; }

        public string AssignedTo { get; set; }
    }
}