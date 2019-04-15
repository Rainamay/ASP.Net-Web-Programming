using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Movie
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int YearRelease { get; set; }
    }
}