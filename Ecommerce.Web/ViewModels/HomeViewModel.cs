using Ecommerce.Entities;
using System.Collections.Generic;

namespace Ecommerce.Web.ViewModel
{
    public class HomeViewModel
    {
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> FeaturedProducts { get; set; }
    }
}