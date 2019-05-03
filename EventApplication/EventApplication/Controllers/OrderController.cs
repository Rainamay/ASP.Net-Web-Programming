using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventApplication.Models;

namespace EventApplication.Controllers
{
    public class OrderController : Controller
    {
        EventDB db = new EventDB();

        // GET: Order
        public ActionResult Index()
        {
            OrderCart cart = OrderCart.GetOrder(this.HttpContext);

            OrderCartViewModel vm = new OrderCartViewModel()
            {
                CartItems = cart.GetCartItems()
            };

            return View(vm);
        }

        public ActionResult Register(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: ShoppingCart/AddToCart
        public ActionResult AddToCart(int id, int count)
        {
            OrderCart cart = OrderCart.GetOrder(this.HttpContext);
            cart.AddToCart(id, count);
            return RedirectToAction("Index");
        }

        // POST: ShoppingCart/RemoveFromCart
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            OrderCart cart = OrderCart.GetOrder(this.HttpContext);

            Event someEvent = db.Orders.SingleOrDefault(c => c.RecordId == id).EventSelected;

            int newItemCount = cart.RemoveFromCart(id);

            OrderCartRemoveViewModel vm = new OrderCartRemoveViewModel()
            {
                DeleteId = id,
                ItemCount = newItemCount,
                Message = someEvent.EventTitle + " has been removed from the cart."
            };

            return Json(vm);
        }
    }
}