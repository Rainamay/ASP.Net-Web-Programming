using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class InvalidCharsAttribute : ValidationAttribute
    {
        readonly char[] badCharacters;

        public InvalidCharsAttribute(string badCharacters) : base("{0} contains unacceptable characters!")
        {
            this.badCharacters = badCharacters.ToCharArray();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                var valueAsString = value.ToString();

                foreach (char ch in badCharacters)
                {
                    if (valueAsString.Contains(ch))
                    {
                        return new ValidationResult(errorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}