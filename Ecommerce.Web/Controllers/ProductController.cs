using Ecommerce.Entities;
using Ecommerce.Services;
using Ecommerce.Web.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string Search, int? PageNo)
        {
            var pageSize = ConfigurationService.Instance.PageSize();
            var Model = new ProductSearchViewModel();
            Model.SearchTerm = Search;
            PageNo = PageNo.HasValue ? PageNo.Value > 0 ? PageNo.Value : 1 : 1;
            var TotalRecords = ProductsService.Instance.GetProductsCount(Search);
            Model.Products = ProductsService.Instance.GetProducts(Search, PageNo.Value , pageSize);
                Model.Pager = new Pager(TotalRecords, PageNo, pageSize);
                return PartialView("ProductTable", Model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var Model = new UpsertProductViewModel();
            Model.Categories = CategoriesService.Instance.GetAllCategories();
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult Create(UpsertProductViewModel Model)
        {
            Product NewProduct = new Product();
            NewProduct.Name = Model.Name;
            NewProduct.Description = Model.Description;
            NewProduct.Price = Model.Price;
            NewProduct.ImageUrl = Model.ImageUrl;
            NewProduct.Category = CategoriesService.Instance.GetCategory(Model.CategoryID);
            ProductsService.Instance.SaveProduct(NewProduct);
            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var Product = ProductsService.Instance.GetProduct(ID);
            var Model = new UpsertProductViewModel();
            Model.ID = Product.ID;
            Model.Name = Product.Name;
            Model.Description = Product.Description;
            Model.Price = Product.Price;
            Model.CategoryID = Product.Category != null ? Product.CategoryID : 0;
            Model.ImageUrl = Product.ImageUrl;
            Model.Categories = CategoriesService.Instance.GetAllCategories();
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult Edit(UpsertProductViewModel Model)
        {
            var ExcistingProduct = ProductsService.Instance.GetProduct(Model.ID);
            ExcistingProduct.Name = Model.Name;
            ExcistingProduct.Description = Model.Description;
            ExcistingProduct.Price = Model.Price;

            ExcistingProduct.Category = null;
            ExcistingProduct.CategoryID = Model.CategoryID;
            if (!string.IsNullOrEmpty(Model.ImageUrl))
            {
                ExcistingProduct.ImageUrl = Model.ImageUrl;
            }
            ProductsService.Instance.UpdateProduct(ExcistingProduct);
            return RedirectToAction("ProductTable");
        }


        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsService.Instance.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }
        public ActionResult Details(int ID)
        {
            var Model = new ProductViewModel();
            Model.Product = ProductsService.Instance.GetProduct(ID);
            if (Model.Product == null) return HttpNotFound();
            return View(Model);
        }
    }
}