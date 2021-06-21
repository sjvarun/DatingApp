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
    [StringLength(8,MinimumLength =4)]
    public string Password { get; set; }
}
