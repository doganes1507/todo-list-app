using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Common.DataObjects.User;

namespace ToDoListApp.Api.Interfaces;

public interface IAuthenticationController
{
    public ActionResult<UserResponseDto> Register(UserRegisterDto newUser);
    public ActionResult<string> Login(UserLoginDto loginUser);
    public IActionResult ChangeUsername(string newUsername);
    public IActionResult ChangePassword(string newPassword);
}