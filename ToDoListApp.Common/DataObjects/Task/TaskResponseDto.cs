namespace ToDoListApp.Common.DataObjects.Task;

public class TaskResponseDto(
    int id,
    string title,
    string? description,
    DateTime creationDate,
    DateTime? dueDate,
    bool isCompleted,
    int taskListId)
{
    public int Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string? Description { get; set; } = description;
    public DateTime CreationDate { get; set; } = creationDate;
    public DateTime? DueDate { get; set; } = dueDate;
    public bool IsCompleted { get; set; } = isCompleted;
    public int TaskListId { get; set; } = taskListId;
}