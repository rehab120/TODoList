using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TODoList.Models
{
    public class Tassk
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        [MinLength(2)]
        public string? Title { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        public string? Description { get; set; }
      
        public Status Status { get; set; } = Status.Pending;

        public DateTime Date { get; set; } 

        public bool IsDone { get; set; } = false;

        [JsonIgnore]
        public string? User_id { get; set; }

        [ForeignKey("User_id")]
        [ValidateNever]
        public User? user { get; set; }


    }
}
