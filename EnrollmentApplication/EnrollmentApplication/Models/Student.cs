using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Student
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
    }
}