using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModel
{
    public class OrdersViewModel
    {
        public List<Order> Orders { get; set; }
        public string UserID { get; set; }
        public Pager Pager { get; set; }
        public string Status { get; set; }
    }

    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public CBUser OrderedBy { get; set; }
        public List<string> AvailableStatus { get; set; }
    }
}