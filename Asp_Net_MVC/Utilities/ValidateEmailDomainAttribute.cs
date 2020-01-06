using System.ComponentModel.DataAnnotations;

namespace Asp_Net_MVC.Utilities
{
    public class ValidateEmailDomainAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidateEmailDomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }

        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split('@');
            return strings[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
