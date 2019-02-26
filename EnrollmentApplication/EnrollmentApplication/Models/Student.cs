using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Student : IValidatableObject
    {
        //[Display(Name = "Student ID")]
        public virtual int StudentId { get; set; }

        [Required(ErrorMessage = "Must enter a First Name.")]
        [StringLength(50, ErrorMessage = "First Name is too long.")]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "Must enter a Last Name.")]
        [StringLength(50, ErrorMessage = "Last Name is too long.")]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        public virtual string Address1 { get; set; }

        public virtual string Address2 { get; set; }

        public virtual string City { get; set; }

        public virtual string Zipcode { get; set; }

        public virtual string State { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Address1 != null && Address2 != null && Address1.Equals(Address2))
            {
                yield return (new ValidationResult("Address2 cannot be the same as Address1.",
                                                    new[] { "Address2" }));
            }

            if(State != null && State.Length != 2)
            {
                yield return (new ValidationResult("Enter a 2 digit State code.",
                                                   new[] { "State" }));
            }

            if (Zipcode != null && Zipcode.Length != 5)
            {
                yield return (new ValidationResult("Enter a 5 digit Zipcode.",
                                                   new[] { "Zipcode" }));
            }

            //throw new NotImplementedException();
        }
    }
}