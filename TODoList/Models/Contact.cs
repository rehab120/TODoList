using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODoList.Models
{
    public class Contact
    {

        [Key]
        public int Id { get; set; }

        
        [MaxLength(20)]
        [MinLength(2)]
        public string? UserName { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Message { get; set; }

        public string? User_id { get; set; }

        [ForeignKey("User_id")]
        [ValidateNever]
        public Users? user { get; set; }

    }
}
