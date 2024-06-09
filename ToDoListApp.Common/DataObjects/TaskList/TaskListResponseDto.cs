namespace ToDoListApp.Common.DataObjects.TaskList;

public class TaskListResponseDto(int id, string title, string? description, int userId)
{
    public int Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string? Description { get; set; } = description;
    public int UserId { get; set; } = userId;
}