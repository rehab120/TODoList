using System.ComponentModel.DataAnnotations;

namespace TODoList.ViewModel
{
    public class ContactViewModel
    {
        
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string UserName { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Message { get; set; }
    }
}
