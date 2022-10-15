﻿using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModel
{
    public class CheckOutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIDs { get; set; }
        public CBUser User { get; set; }
    }
    public class ShopViewModel
    {
        public int MaximumPrice { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }
        public int? PageNo { get; set; }
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
    }
    public class FilterProductsViewModel
    {
        public List<Product> Products { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }
        public string SearchTerm { get; set; }
    }
}
