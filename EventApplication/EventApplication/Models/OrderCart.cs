using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApplication.Models
{
    public class OrderCart
    {
        public string OrderCartId;

        private EventDB db = new EventDB();

        public static OrderCart GetOrder(HttpContextBase context)
        {
            OrderCart order = new OrderCart();
            order.OrderCartId = order.GetOrderCartId(context);
            return order;
        }

        private string GetOrderCartId (HttpContextBase context) {

            const string CartSessionKey = "CartId";
            string cartId;

            if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
            {
                context.Session[CartSessionKey] = context.User.Identity.Name;
                cartId = context.User.Identity.Name;
            }
            else
            {
                 cartId = context.User.Identity.Name.ToString();
            }
            return cartId;
        }

        public int AddToCart(int eventId, int count)
        {
            // TO DO: Verify that the Album Id exists in the database.
            Order cartItem = db.Orders.SingleOrDefault(c => c.CartId == this.OrderCartId && c.EventId == eventId);
            Event theEvent = db.Events.Find(eventId);

            if (cartItem == null)
            {
                Random r = new Random();
                int n = r.Next();
                // Item is not in cart; add new cart item
                cartItem = new Order()
                {
                    RecordNum = n,
                    CartId = this.OrderCartId,
                    EventId = eventId,
                    Count = count,
                    DateCreated = DateTime.Now
                };

                db.Orders.Add(cartItem);
            }
            else
            {
                // Item is already in cart; increase item count (quantity)
                cartItem.Count = cartItem.Count + count;
            }
            theEvent.AvailableTickets = theEvent.AvailableTickets - count;
            db.SaveChanges();
            return cartItem.RecordNum;
        }

        public List<Order> GetCartItems()
        {
            return db.Orders.Where(c => c.CartId == this.OrderCartId).ToList();
        }

        public void RemoveFromCart(int recordId)
        {
            Order cartItem = db.Orders.SingleOrDefault(c => c.CartId == this.OrderCartId && c.RecordId == recordId);

            if (cartItem == null)
            {
                throw new NullReferenceException();
            }

            int itemCount = cartItem.Count;
            Event orderEvent = cartItem.EventSelected;

            orderEvent.AvailableTickets = orderEvent.AvailableTickets + itemCount;
            
            db.Orders.Remove(cartItem);

            db.SaveChanges();
        }
    }

}