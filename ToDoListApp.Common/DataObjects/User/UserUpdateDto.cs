using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Common.DataObjects.User;

public class UserUpdateDto
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string FirstName { get; set; }
}