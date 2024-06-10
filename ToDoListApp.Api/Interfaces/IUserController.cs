using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Common.DataObjects.User;

namespace ToDoListApp.Api.Interfaces;

public interface IUserController
{
    public ActionResult<UserResponseDto> GetUser(int id);
    public ActionResult<UserResponseDto> UpdateUser(int id, UserUpdateDto newUser);
    public ActionResult<UserResponseDto> DeleteUser(int id);
}