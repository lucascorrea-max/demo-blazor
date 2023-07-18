using System.ComponentModel.DataAnnotations;

public class LoginModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Password { get; set; } = string.Empty;
}
