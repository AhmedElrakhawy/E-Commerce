using Ecommerce.Database;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class ShopService
    {
        public static ShopService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ShopService();
                return instance;
            }
        }
        private static ShopService instance { set; get; }

        private ShopService()
        {

        }

        public int Save(Order order)
        {
            using (var Context = new CBContext())
            {
                Context.Orders.Add(order);
               return Context.SaveChanges();
            }
        }
    }
}
