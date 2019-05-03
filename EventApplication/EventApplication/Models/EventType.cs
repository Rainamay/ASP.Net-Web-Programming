using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventApplication.Models
{
    public class EventType
    {
        public virtual int EventTypeId { get; set; }

        [Required(ErrorMessage = "You must enter a type.")]
        [StringLength(50, ErrorMessage = "The type is too long.")]
        public virtual string Type { get; set; }
    }
}