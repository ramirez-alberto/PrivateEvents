using System.ComponentModel.DataAnnotations;

namespace PrivateEvents.Entities.Models;

public class UserRegistrationModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(20, ErrorMessage = "Password must be 20 characters long max")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}