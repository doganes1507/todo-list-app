using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ToDoListApp.Api.Interfaces;
using ToDoListApp.Api.Models;
using ToDoListApp.Common.DataObjects.User;

namespace ToDoListApp.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class JwtAuthenticationController(IRepository<User> userRepository, IConfiguration configuration, IMapper mapper)
    : ControllerBase, IAuthenticationController
{
    [HttpPost("register")]
    public ActionResult<UserResponseDto> Register(UserRegisterDto newUser)
    {
        if (userRepository.Find(user => user.Username == newUser.Username) is not null)
            return BadRequest("User with this username already exists");

        var user = new User
        {
            Username = newUser.Username,
            HashPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password),
            FirstName = newUser.FirstName
        };

        userRepository.Add(user);

        var response = mapper.Map<UserResponseDto>(user);
        return Ok(response);
    }

    [HttpPost("login")]
    public ActionResult<string> Login(UserLoginDto loginUser)
    {
        var user = userRepository.Find(user => user.Username == loginUser.Username);

        if (user is null)
            return NotFound();

        if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, user.HashPassword))
            return Unauthorized();

        return Ok(CreateToken(user));
    }

    [Authorize]
    [HttpPost("change-username")]
    public IActionResult ChangeUsername(string newUsername)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var user = userRepository.GetById(int.Parse(idFromToken));
        if (user is null)
            return NotFound();

        if (userRepository.Find(dbUser => dbUser.Username == newUsername) is not null)
            return BadRequest("User with this username already exists");

        user.Username = newUsername;
        userRepository.Update(user);

        return Ok();
    }

    [Authorize]
    [HttpPost("change-password")]
    public IActionResult ChangePassword(string newPassword)
    {
        var idFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idFromToken is null)
            return Unauthorized();

        var user = userRepository.GetById(int.Parse(idFromToken));
        if (user is null)
            return NotFound();

        user.HashPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
        userRepository.Update(user);

        return Ok();
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}