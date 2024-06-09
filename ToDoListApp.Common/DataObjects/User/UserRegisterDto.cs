﻿using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Common.DataObjects.User;

public class UserRegisterDto
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Username { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 8)]
    public string Password { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string FirstName { get; set; }
}