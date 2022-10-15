using Ecommerce.Services;
using Ecommerce.Web.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class OrderController : Controller
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

        // GET: Order
        public ActionResult Index(string UserID, string Status, int? pageNo)
        {
            var Model = new OrdersViewModel();

            var pageSize = ConfigurationService.Instance.PageSize();
            Model.UserID = UserID;
            Model.Status = Status;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo : 1 : 1;

            Model.Orders = OrdersService.Instance.FilterOrders(UserID, Status, pageNo.Value, pageSize);

            var TotalRecords = OrdersService.Instance.FilterOrdersCount(UserID, Status);
            Model.Pager = new Pager(TotalRecords, pageNo, pageSize);

            return View(Model);
        }

        public ActionResult Details(int ID)
        {
            OrderDetailsViewModel Model = new OrderDetailsViewModel();
            Model.Order = OrdersService.Instance.GetOrderByID(ID);
            if (Model.Order != null)
            {
                Model.OrderedBy = UserManager.FindById(Model.Order.UserId);
            }

            Model.AvailableStatus = new List<string>() { "Pending" , "In Progress" , "Delivered" };
            return View(Model);
        }

        public JsonResult ChangeStatus(string Status , int ID)
        {
            var Result = new JsonResult();
            Result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            Result.Data = new { Success = OrdersService.Instance.UpdateStatus(Status , ID)};

            return Result;
        }
    }
}