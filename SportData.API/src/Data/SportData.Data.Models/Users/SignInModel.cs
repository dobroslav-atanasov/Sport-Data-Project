namespace SportData.Data.Models.Users;

using System.ComponentModel.DataAnnotations;

public class SignInModel
{
    [Required(ErrorMessage = "Username is required!")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required!")]
    public string Password { get; set; }
}