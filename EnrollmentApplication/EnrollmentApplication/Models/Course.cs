using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Course
    {
        //[Display(Name = "Course ID")]
        public virtual int CourseId { get; set; }

        [Required(ErrorMessage = "Must enter a Course Title.")]
        [StringLength(150, ErrorMessage = "Course Title is too long.")]
        [Display(Name = "Course Title")]
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        [Required(ErrorMessage = "Must enter the number of credits.")]
        [Range(typeof(decimal), "1.00", "4.00", ErrorMessage = "Credits must be a number from 1 to 4.")]
        [Display(Name = "Number of credits")]
        public virtual decimal Credits { get; set; }
    }
}