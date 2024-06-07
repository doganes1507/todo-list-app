using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Api.Models;

public class Task
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    [Required]
    public bool IsCompleted { get; set; }
    
    [ForeignKey("TaskList")]
    public int TaskListId { get; set; }
    
    public virtual TaskList TaskList { get; set; }
}