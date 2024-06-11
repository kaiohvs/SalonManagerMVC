using System.ComponentModel.DataAnnotations;

namespace SalonManager.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Usuario nao digitado! ")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Senha nao digitada! ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
