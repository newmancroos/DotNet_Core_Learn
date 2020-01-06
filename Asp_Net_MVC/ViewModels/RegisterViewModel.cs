using Asp_Net_MVC.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Asp_Net_MVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action:"IsEmailInUse",controller:"Account")]
        [ValidateEmailDomain(allowedDomain: "test.com", ErrorMessage ="Email domain must be test.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage ="Password and Confirm password do not match.")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
    }
}
