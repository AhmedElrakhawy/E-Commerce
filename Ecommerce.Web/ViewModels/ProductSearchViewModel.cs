using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModel
{
    public class ProductSearchViewModel
    {
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
        public List<Product> Products { get; set; }
    }
}