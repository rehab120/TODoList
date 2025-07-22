using System.ComponentModel.DataAnnotations;

namespace TODoList.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
