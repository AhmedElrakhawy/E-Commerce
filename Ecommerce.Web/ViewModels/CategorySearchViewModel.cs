using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModel
{
    public class CategorySearchViewModel
    {
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
        public List<Category> Categories { get; set; }
    }
}