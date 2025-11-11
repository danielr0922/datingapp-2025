using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto //What we use so we can convert the body for the Acoount Controller to use
{
    [Required] //required data annotation
    public string DisplayName { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [MinLength(4)]
    public string Password { get; set; } = "";
}
