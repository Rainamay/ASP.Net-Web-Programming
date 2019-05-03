using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventApplication.Models;

namespace EventApplication.Controllers
{
    public class HomeController : Controller
    {
        private EventDB db = new EventDB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FindEvent()
        {
            return View();
        }

        public ActionResult LastMinuteDeals()
        {
            var eventsSoon = GetLastMinuteDeals();

            return PartialView("_LastMinuteDeals", eventsSoon);
        }

        private List<Event> GetLastMinuteDeals()
        {
            DateTime today = DateTime.Now;
            DateTime twoDaysAway = today.AddDays(2);

            var eventsSoon = db.Events.Where(a => a.StartDate.CompareTo(twoDaysAway) <= 0);

            return eventsSoon.ToList();
        }

        public ActionResult EventSearch(string q, string r)
        {
            var events = GetEvents(q, r);
            return PartialView("_EventSearchResults", events);
        }

        private List<Event> GetEvents(string searchString, string searchString2)
        {
            return db.Events
                .Where(a => 
                (a.EventTitle.Contains(searchString) || a.EventType.Type.Contains(searchString)) && 
                (a.City.Contains(searchString2) || a.State.Contains(searchString2)))
                        .ToList();
        }
    }
}