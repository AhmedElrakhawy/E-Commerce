using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModel
{
    public class UpsertProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int CategoryID { get; set; }
        public string ImageUrl { get; set; }
        public List<Category> Categories { get; set; }
    }
    public class ProductViewModel
    {
        public Product Product { get; set; }
    }
}