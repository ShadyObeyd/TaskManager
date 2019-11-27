namespace TaskManager.Models.DataModels
{
    using System;
    public class BaseModel
    {
        public BaseModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
    }
}
