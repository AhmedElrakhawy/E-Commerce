using Ecommerce.Entities;
using Ecommerce.Services;
using Ecommerce.Web.ViewModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryTable(string Search, int? PageNo)
        {
            var Model = new CategorySearchViewModel();
            Model.SearchTerm = Search;
            PageNo = PageNo.HasValue ? PageNo.Value > 0 ? PageNo.Value : 1 : 1;
            var TotalRecords = CategoriesService.Instance.GetCategoriesCount(Search);
            Model.Categories = CategoriesService.Instance.GetCategories(Search, PageNo.Value);
            if (Model.Categories != null)
            {
                Model.Pager = new Pager(TotalRecords, PageNo, 3);
                return PartialView("CategoryTable", Model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            var Model = new NewCategoryViewModel();
            return PartialView(Model);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel Model)
        {
            
            if (ModelState.IsValid)
            {
                var NewCategory = new Category();
                NewCategory.Name = Model.Name;
                NewCategory.Description = Model.Description;
                NewCategory.IsFeatured = Model.IsFeatured;
                NewCategory.ImageUrl = Model.ImageUrl;
                CategoriesService.Instance.Save(NewCategory);
                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoriesService.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageUrl = category.ImageUrl;
            model.IsFeatured = category.IsFeatured;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel Model)
        {
            var Category = CategoriesService.Instance.GetCategory(Model.ID);
            Category.Name = Model.Name;
            Category.Description = Model.Description;
            Category.IsFeatured = Model.IsFeatured;
            Category.ImageUrl = Model.ImageUrl;

            CategoriesService.Instance.UpdateCategory(Category);
            return RedirectToAction("CategoryTable");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            CategoriesService.Instance.DeleteCategory(ID);

            return RedirectToAction("CategoryTable");
        }
    }
}