using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalonManager.Web.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Por favor, preencha o nome do usuario! ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o email de usuario! ")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, preencha a senha de usuario! ")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "A senha deve ter entre 3 e 10 caracteres")]
        public string PasswordHash { get; set; }                
        public bool IsAdmin { get; set; } 
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }   
        
    }
}
