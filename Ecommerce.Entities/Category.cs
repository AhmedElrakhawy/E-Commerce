using System.Collections.Generic;

namespace Ecommerce.Entities
{
    public class Category : BaseEntity
    {
        public string ImageUrl { get; set; }
        public List<Product> Products { get; set; }
        public bool IsFeatured { get; set; }
    }
}