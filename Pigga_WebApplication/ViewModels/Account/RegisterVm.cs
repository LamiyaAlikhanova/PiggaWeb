using System.ComponentModel.DataAnnotations;

namespace Pigga_WebApplication.ViewModels.Account
{
    public class RegisterVm
    {
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        public string Surname { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
