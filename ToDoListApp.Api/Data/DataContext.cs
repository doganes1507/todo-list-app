using Microsoft.EntityFrameworkCore;
using ToDoListApp.Api.Models;
using Task = ToDoListApp.Api.Models.Task;
namespace ToDoListApp.Api.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskList> TaskLists { get; set; }
    public DbSet<Task> Tasks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.TaskLists)
            .WithOne(tl => tl.User)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskList>()
            .HasMany(tl => tl.Tasks)
            .WithOne(t => t.TaskList)
            .OnDelete(DeleteBehavior.Cascade);
    }
}