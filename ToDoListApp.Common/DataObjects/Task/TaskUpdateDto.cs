using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Common.DataObjects.Task;

public class TaskUpdateDto
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    [Required]
    public bool IsCompleted { get; set; }
}