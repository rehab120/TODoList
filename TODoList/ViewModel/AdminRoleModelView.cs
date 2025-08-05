using System.ComponentModel.DataAnnotations;

namespace TODoList.ViewModel
{
    public class AdminRoleModelView
    {

        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
       
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

    }
}
