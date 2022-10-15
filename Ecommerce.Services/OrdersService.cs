using Ecommerce.Database;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class OrdersService
    {
        public static OrdersService Instance
        {
            get
            {
                if (instance == null)
                    instance = new OrdersService();
                return instance;
            }
        }
        private static OrdersService instance { set; get; }

        private OrdersService()
        {

        }

        public List<Order> FilterOrders(string UserID, string Status, int pageNo, int pageSize)
        {
            using (var context = new CBContext())
            {
                var Orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(UserID))
                {
                    Orders = Orders.Where(x => x.UserId == UserID).ToList();
                }

                if (!string.IsNullOrEmpty(Status))
                {
                    Orders = Orders.Where(x => x.Status.ToLower().Contains(Status.ToLower())).ToList();
                }

                return Orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int FilterOrdersCount(string UserID ,string Status)
        {
            using (var context = new CBContext())
            {
                var Orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(UserID))
                {
                    Orders = Orders.Where(x => x.UserId == UserID).ToList();
                }

                if (!string.IsNullOrEmpty(Status))
                {
                    Orders = Orders.Where(x => x.Status.ToLower().Contains(Status.ToLower())).ToList();
                }

                return Orders.Count();
            }
        }

        public Order GetOrderByID(int ID)
        {
            using (var Context = new CBContext())
            {
                return Context.Orders.Where(x => x.ID == ID).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();
            }
        }
        public int Save(Order order)
        {
            using (var Context = new CBContext())
            {
                Context.Orders.Add(order);
               return Context.SaveChanges();
            }
        }

        public bool UpdateStatus(string Status, int ID)
        {
            using (var Context = new CBContext())
            {
                var Order = Context.Orders.Find(ID);
                Order.Status = Status;
                Context.Entry(Order).State = EntityState.Modified;
                return Context.SaveChanges() > 0;
            }
        }
    }
}
