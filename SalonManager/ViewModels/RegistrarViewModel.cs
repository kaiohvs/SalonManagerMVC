using System.ComponentModel.DataAnnotations;

namespace SalonManager.Web.ViewModels
{
    public class RegistrarViewModel
    {
        [Required(ErrorMessage = "Por favor, preencha o nome do usuário!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o email do usuário!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, preencha a senha do usuário!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "A senha deve ter entre 3 e 10 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
