using Microsoft.EntityFrameworkCore;

namespace ApiProject.Data
{
    public class ApiProjectDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApiProjectDbContext(DbContextOptions<ApiProjectDbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        /* Making sure that the composite key is created ProjectMember (ProjectId,UserId) */
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProjectMember>().HasKey(table => new {
                table.ProjectId,
                table.UserId
            });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
    }
}
