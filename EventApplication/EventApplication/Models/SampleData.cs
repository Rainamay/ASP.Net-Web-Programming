using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EventApplication.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<EventDB>
    {
        protected override void Seed(EventDB context)
        {
            var eventTypes = new List<EventType>
            {
                new EventType { Type = "Concert" },
                new EventType { Type = "Carnival" },
                new EventType { Type = "Graduation Party" },
                new EventType { Type = "Formal" },
                new EventType { Type = "Basketball Game" }
            };

            new List<Event>
            {
                new Event { EventType = eventTypes.Single(g => g.Type == "Concert"), EventTitle = "Katy Perry Concert", EventDescription = "A Katy Perry concert in downtown Cleveland.", StartDate = new DateTime(2019, 5, 13, 7, 0, 0), EndDate = new DateTime(2019, 5, 15, 11, 0, 0), City = "Cleveland", State = "OH", MaxTickets = 300, AvailableTickets = 10}
            }.ForEach(e => context.Events.Add(e));
        }
    }
}