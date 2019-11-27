namespace TaskManager.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models.DataModels;
    public class TaskManagerContext : IdentityDbContext<TaskManagerUser>
    {
        public TaskManagerContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<TaskType> TaskTypes { get; set; }

        public DbSet<TaskStatus> TaskStatuses { get; set; }

        public DbSet<CommentType> CommentTypes { get; set; }

        public DbSet<UserTask> UsersTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserTask>().HasKey(ut => new { ut.UserId, ut.TaskId });
            base.OnModelCreating(builder);
        }
    }
}