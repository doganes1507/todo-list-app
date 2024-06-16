namespace ToDoListApp.Common.DataObjects.TaskList;

public class TaskListResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
}