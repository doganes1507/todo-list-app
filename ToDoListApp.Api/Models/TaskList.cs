using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Api.Models;

public class TaskList
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    public virtual User User { get; set; }
    
    public virtual ICollection<Task> Tasks { get; set; }
}