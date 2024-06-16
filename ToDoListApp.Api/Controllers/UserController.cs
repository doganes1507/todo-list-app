using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Api.Interfaces;
using ToDoListApp.Api.Models;
using ToDoListApp.Common.DataObjects.User;

namespace ToDoListApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(IRepository<User> userRepository, IMapper mapper) : ControllerBase, IUserController
{
    [HttpGet("{id}")]
    public ActionResult<UserResponseDto> GetUser(int id)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();
        
        if (id != int.Parse(idFromToken))
            return Forbid();
        
        var user = userRepository.GetById(int.Parse(idFromToken));
        if (user is null)
            return NotFound();

        var response = mapper.Map<UserResponseDto>(user);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public ActionResult<UserResponseDto> UpdateUser(int id, UserUpdateDto newUser)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();
        
        if (id != int.Parse(idFromToken))
            return Forbid();
        
        var user = userRepository.GetById(int.Parse(idFromToken));
        if (user is null)
            return NotFound();

        mapper.Map(newUser, user);
        userRepository.Update(user);
        
        var response = mapper.Map<UserResponseDto>(user);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult<UserResponseDto> DeleteUser(int id)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();
        
        if (id != int.Parse(idFromToken))
            return Forbid("");
        
        var user = userRepository.GetById(int.Parse(idFromToken));
        if (user is null)
            return NotFound();
        
        userRepository.Remove(user);
        
        var response = mapper.Map<UserResponseDto>(user);
        return Ok(response);
    }
}