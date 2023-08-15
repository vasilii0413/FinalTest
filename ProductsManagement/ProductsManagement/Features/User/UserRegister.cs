using System.ComponentModel.DataAnnotations;

namespace ProductsManagement.Features.User
{
    public class UserRegister
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50,MinimumLength =5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50,MinimumLength =5)]
        public string ConfirmPassword { get; set; }
    }
}
