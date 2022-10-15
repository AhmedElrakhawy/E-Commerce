using Ecommerce.Web.ViewModel;
using Ecommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLatestProducts, int? CategoryID)
        {
            var Model = new ProductsWidgetViewModel();

            Model.IsLatestProducts = isLatestProducts;

            if (isLatestProducts)
            {
                Model.Products = ProductsService.Instance.GetLatestProducts(4);
            }
            else if(CategoryID.HasValue && CategoryID > 0)
            {
                Model.Products = ProductsService.Instance.GetProductsByCategoryID(CategoryID.Value,4);
            }
            else
            {
                Model.Products = ProductsService.Instance.GetProducts(1, 8);
            }
            return PartialView(Model);
        }
    }
}