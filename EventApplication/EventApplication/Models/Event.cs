using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventApplication.Models
{
    public class Event
    {
        public virtual int EventId { get; set; }

        public virtual int EventTypeId { get; set; }
        
        public virtual EventType EventType { get; set; }

        [Required(ErrorMessage = "You must enter an event title.")]
        [StringLength(50, ErrorMessage = "The event title is too long.")]
        [Display(Name ="Event Title")]
        public virtual string EventTitle { get; set; }

        [StringLength(150, ErrorMessage = "The event description is too long.")]
        [Display(Name = "Description")]
        public virtual string EventDescription { get; set; }

        [Required(ErrorMessage = "You must enter a start date.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [PastDate(ErrorMessage = "Start date cannot be in the past.")]
        [Display(Name = "Start Date")]
        public virtual DateTime StartDate { get; set; }

        [Required(ErrorMessage = "You must enter an end date.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        [PastDate(ErrorMessage = "End Date cannot be in the past.")]
        [Display(Name = "End Date")]
        public virtual DateTime EndDate { get; set; }

        [Required(ErrorMessage = "You must enter a city.")]
        public virtual string City { get; set; }

        [Required(ErrorMessage = "You must enter a state.")]
        public virtual string State { get; set; }

        [Required(ErrorMessage = "You must enter the name of the event organizer.")]
        [Display(Name = "Organizer Name")]
        public virtual string OrganizerName { get; set; }

        [Display(Name = "Organizer Contact Info")]
        public virtual string OrganizerContact { get; set; }

        [Required(ErrorMessage = "You must enter a maximum ticket amount.")]
        [Range(1, int.MaxValue, ErrorMessage = "The maximum ticket amount must be greater than 0.")]
        [Display(Name = "Max Tickets")]
        public virtual int MaxTickets { get; set; }

        [Required(ErrorMessage = "You must enter the amount of available tickets.")]
        [Range(1, int.MaxValue, ErrorMessage = "The available ticket amount must be greater than 0.")]
        [Display(Name = "Available Tickets")]
        public virtual int AvailableTickets { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate != null && EndDate != null && StartDate > EndDate)
            {
                yield return (new ValidationResult("The end date cannot come before the start date.",
                                                    new[] { "EndDate" }));
            }
        }

    }
}