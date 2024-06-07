using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Api.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Username { get; set; }

    [Required]
    [StringLength(256)]
    public string HashPassword { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string FirstName { get; set; }
    
    public virtual ICollection<TaskList> TaskLists { get; set; }
}