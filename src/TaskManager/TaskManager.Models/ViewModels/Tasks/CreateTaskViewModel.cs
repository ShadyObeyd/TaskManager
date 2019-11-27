namespace TaskManager.Models.ViewModels.Tasks
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTaskViewModel
    {
        private const string EmailValidationRegex = @"^(\s?[^\s,]+@[^\s,]+\.[^\s,]+\s?,)*(\s?[^\s,]+@[^\s,]+\.[^\s,]+)$";
        private const string StatusAndTypeValidationRegex = @"^[^,]+,.*[^,]$";
        [Required]
        [Display(Name = "Required by")]
        public string RequiredByDate { get; set; }

        [Required]
        [Display(Name = "Task Description")]
        public string TaskDescription { get; set; }

        [Required]
        [Display(Name = "Task Statuses")]
        [RegularExpression(StatusAndTypeValidationRegex, ErrorMessage = "Please write the statuses separated by comma!")]
        public string TaskStatus { get; set; }

        [Required]
        [Display(Name = "Task Types")]
        [RegularExpression(StatusAndTypeValidationRegex, ErrorMessage = "Please write the types separated by comma!")]
        public string TaskType { get; set; }
        [Required]
        [Display(Name = "Assigned to")]
        [RegularExpression(EmailValidationRegex, ErrorMessage = "Please write the emails separated by comma!")]
        public string AssignedTo { get; set; }
    }
}