using System.ComponentModel.DataAnnotations;

namespace Asp_Net_MVC.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
