
namespace TaskManager.Models.ViewModels.Tasks
{
    using System.Collections.Generic;
    public class IndexTaskListViewModel
    {
        public IndexTaskListViewModel()
        {
            this.Tasks = new HashSet<IndexTaskViewModel>();
        }
        public ICollection<IndexTaskViewModel> Tasks { get; set; }
    }
}