namespace TaskManager.Models.ViewModels.Tasks
{
    public class IndexTaskViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public string PartialContent => this.GetPartialContent();

        private string GetPartialContent()
        {
            if (Content.Length <= 100)
            {
                return this.Content;
            }
            else
            {
                return this.Content.Substring(0, 100) + "...";
            }
        }
    }
}