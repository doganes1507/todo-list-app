namespace ToDoListApp.Common.DataObjects.Task;

public class TaskResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public int TaskListId { get; set; }
}