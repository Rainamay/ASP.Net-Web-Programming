using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventApplication.Models;

namespace EventApplication.Controllers
{
    [Authorize]
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

        public ActionResult OrderSummary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order @order = db.Orders.Where(d => d.RecordNum == id).First();
            if (@order == null)
            {
                return HttpNotFound();
            }
            return View(@order);
        }

        // GET: ShoppingCart/AddToCart
        public ActionResult AddToCart(int id, int count)
        {
            OrderCart cart = OrderCart.GetOrder(this.HttpContext);
            int ordernum = cart.AddToCart(id, count);
            return RedirectToAction("OrderSummary", new { id = ordernum });
        }

        // POST: Order/RemoveFromCart
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            OrderCart cart = OrderCart.GetOrder(this.HttpContext);

            Event someEvent = db.Orders.SingleOrDefault(c => c.RecordId == id).EventSelected;

            cart.RemoveFromCart(id);

            OrderCartRemoveViewModel vm = new OrderCartRemoveViewModel()
            {
                DeleteId = id,
                Message = "Your order has been cancelled."
            };

            return Json(vm);
        }
    }
}