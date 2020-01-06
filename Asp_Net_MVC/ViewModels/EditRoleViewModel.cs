using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asp_Net_MVC.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Role name is Required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; } = new List<string>();

    }
}
