using System.ComponentModel.DataAnnotations;

namespace TODoList.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Compare("Password")]

        public string? ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}
