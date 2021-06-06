using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Summary description for Class1
/// </summary>
public class RegisterDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
