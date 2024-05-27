using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalonManager.Web.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? PasswordHash { get; set; }
    }
}
