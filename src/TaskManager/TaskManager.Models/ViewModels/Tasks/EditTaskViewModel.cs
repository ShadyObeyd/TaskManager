using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.ViewModels.Tasks
{
    public class EditTaskViewModel
    {
        private const string EmailValidationRegex = @"^(\s?[^\s,]+@[^\s,]+\.[^\s,]+\s?,)*(\s?[^\s,]+@[^\s,]+\.[^\s,]+)$";
        private const string StatusAndTypeValidationRegex = @"^[^,]+,.*[^,]$";

        public string Id { get; set; }
        public string Creator { get; set; }

        [Required]
        [Display(Name = "Task Description")]
        public string Content { get; set; }

        public string DateCreated { get; set; }

        [Required]
        [Display(Name = "Required by")]
        public string RequiredByDate { get; set; }

        [Required]
        [Display(Name = "Task Statuses")]
        [RegularExpression(StatusAndTypeValidationRegex, ErrorMessage = "Please write the statuses separated by comma!")]
        public string Statuses { get; set; }

        [Required]
        [Display(Name = "Task Types")]
        [RegularExpression(StatusAndTypeValidationRegex, ErrorMessage = "Please write the types separated by comma!")]
        public string Types { get; set; }

        [Required]
        [Display(Name = "Assigned to")]
        [RegularExpression(EmailValidationRegex, ErrorMessage = "Please write the emails separated by comma!")]
        public string AssignedTo { get; set; }
    }
}