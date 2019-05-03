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

        public void AddToCart(int eventId, int count)
        {
            // TO DO: Verify that the Album Id exists in the database.
            Order cartItem = db.Orders.SingleOrDefault(c => c.CartId == this.OrderCartId && c.EventId == eventId);

            if (cartItem == null)
            {
                // Item is not in cart; add new cart item
                cartItem = new Order()
                {
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

            db.SaveChanges();
        }

        public List<Order> GetCartItems()
        {
            return db.Orders.Where(c => c.CartId == this.OrderCartId).ToList();
        }

        public int RemoveFromCart(int recordId)
        {
            Order cartItem = db.Orders.SingleOrDefault(c => c.CartId == this.OrderCartId && c.RecordId == recordId);

            if (cartItem == null)
            {
                throw new NullReferenceException();
            }

            int newCount;

            if (cartItem.Count > 1)
            {
                // Count is greater than 1; Decrement count
                cartItem.Count--;
                newCount = cartItem.Count;
            }
            else
            {
                // Count is at 0; Remove cart item
                db.Orders.Remove(cartItem);
                newCount = 0;
            }

            db.SaveChanges();

            return newCount;
        }
    }

}