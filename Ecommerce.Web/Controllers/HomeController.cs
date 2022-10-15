using Ecommerce.Services;
using Ecommerce.Web.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private CBSignInManager _signInManager;
        private CBUserManager _userManager;

        public CBSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<CBSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public CBUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<CBUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var Model = new HomeViewModel()
            {
                FeaturedCategories = CategoriesService.Instance.GetAllCategories(),
                FeaturedProducts = ProductsService.Instance.GetProducts(1)
            };
            return View(Model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            return RedirectToAction("Index");
        }
    }
}