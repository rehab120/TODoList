using System.ComponentModel.DataAnnotations;

namespace TODoList.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public String RoleName { get; set; }
    }
}
