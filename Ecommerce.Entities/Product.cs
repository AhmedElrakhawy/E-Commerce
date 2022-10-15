using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities
{
    public class Product : BaseEntity
    {
        [Range(1,200000)]
        public decimal Price { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
