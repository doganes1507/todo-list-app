using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Common.DataObjects.TaskList;

public class TaskListRequestDto
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    
    [StringLength(500)]
    public string Description { get; set; }
}