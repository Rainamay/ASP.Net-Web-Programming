﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventApplication.Models
{
    public class Order
    {
        [Key]
        public int RecordId { get; set; }

        public int RecordNum { get; set; }

        public string CartId { get; set; }

        public int EventId { get; set; }

        public virtual Event EventSelected { get; set; }

        public int Count { get; set; }

        public DateTime DateCreated { get; set; }
    }
}