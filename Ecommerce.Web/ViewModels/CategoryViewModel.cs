using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModel
{
    public class CategoryViewModel
    {
        public string SearchTerm { get; set; }
        public List<Category> Categories { get; set; }
    }
    public class EditCategoryViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeatured { get; set; }
    }
}