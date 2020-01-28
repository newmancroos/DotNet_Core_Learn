using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asp_Net_MVC.ViewModels
{
    public class EditUserViewModel
    {
        public string  Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        public string City { get; set; }
        public List<string> Claims { get; set; } = new List<string>();
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
