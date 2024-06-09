namespace ToDoListApp.Common.DataObjects.User;

public class UserResponseDto(int id, string username, string firstName)
{
    public int Id { get; set; } = id;
    public string Username { get; set; } = username;
    public string FirstName { get; set; } = firstName;
}