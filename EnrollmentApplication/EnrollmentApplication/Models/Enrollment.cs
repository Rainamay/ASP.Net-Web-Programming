using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Models
{
    public class Enrollment
    {
        public virtual int EnrollmentId { get; set; }

        [Display(Name = "Student ID")]
        public virtual int StudentId { get; set; }

        [Display(Name = "Course ID")]
        public virtual int CourseId { get; set; }

        [Required(ErrorMessage = "Must enter a Grade.")]
        [RegularExpression(@"[A-Fa-f]", ErrorMessage = "Grade must be a letter from A to F.")]
        public virtual char Grade { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual Boolean IsActive { get; set; }

        [Required(ErrorMessage = "Must enter an Assigned Campus.")]
        [Display(Name = "Assigned Campus")]
        public virtual string AssignedCampus { get; set; }

        [Required(ErrorMessage = "Must enter an Enrollment Semester.")]
        [Display(Name = "Enrolled in Semester")]
        public virtual string EnrollmentSemester { get; set; }

        [Required(ErrorMessage = "Must enter an Enrollment Year.")]
        [Range(2018, int.MaxValue, ErrorMessage = "The Enrollment Year must be 2018 or later.")]
        public virtual int EnrollmentYear { get; set; }
    }
}