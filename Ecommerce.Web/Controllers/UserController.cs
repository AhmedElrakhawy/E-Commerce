using Ecommerce.Database;
using Ecommerce.Entities;
using Ecommerce.Services;
using Ecommerce.Web.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class UserController : Controller
    {
        UserService userService = new UserService();
        private CBSignInManager _signInManager;
        private CBUserManager _userManager;
        private CBRoleManager _roleManager;
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
        public CBRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<CBRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public UserController()
        { }
        public UserController(CBUserManager userManager, CBRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        // GET: User
        public ActionResult Profile()
        {
            var Model = new UserProfileViewModel();
            Model.User = UserManager.FindById(User.Identity.GetUserId());
            return View(Model);
        }
        public JsonResult Update(UserUpdateViewModel Model)
        {
            var Result = new JsonResult();
            Result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (ModelState.IsValid)
            {
                CBUser User = UserManager.FindById(Model.Id);
                User.ImageUrl = Model.ImageUrl;
                User.PhoneNumber = Model.PhoneNumber;
                User.UserName = Model.UserName;
                User.Email = Model.Email;
                User.Name = Model.Name;
                User.Address = Model.Address;
                using (var Context = new CBContext())
                {
                    UserManager.Update(User);
                    Context.SaveChanges();
                }
                Result.Data = new { Success = true };
            }
            else
            {
                Result.Data = new { Success = false };
            }
            return Result;
        }
        public ActionResult UserRole()
        {
            var Model = new UserRolesViewModel();
            //Model.AvailableRoles = RoleManager.Roles.ToList();
            //Model.Users = UserManager.Users.ToList(); 
            return View(Model);
        }

        public ActionResult Manage(string userId)
        {
            using (var Context = new CBContext())
            {
                var user = UserManager.FindById(userId);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Name = user.Name;
                var model = new ManageUserRolesViewModel();
                model.UserId = userId;
                model.Roles = Context.Roles.Select(x => x.Name).ToList();
                foreach (var role in Context.Roles)
                {
                    model.RoleName = role;
                    if (UserManager.IsInRole(userId, role.Name))
                    {
                        model.RoleId = role.Id;
                        model.Selected = true;
                    }
                    else
                    {
                        model.Selected = false;
                    }
                }
                return View(model);
            }

        }
        [HttpPost]
        public JsonResult Manage(ChangeRoleViewModel Model)
        {
            var Result = new JsonResult();
            Result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var roles = UserManager.GetRoles(Model.ID).ToArray();
            var result = UserManager.RemoveFromRoles(Model.ID, roles);
            using (var Context = new CBContext())
            {
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot remove user existing roles");
                    return Result;
                }
                if (roles.Length > 0)
                {
                    foreach (var role in roles)
                    {
                        UserManager.RemoveFromRole(Model.ID, role);
                    }
                }
                result = UserManager.AddToRole(Model.ID, Model.RoleName.Remove(0, 9));
                Result.Data = new { Success = true };
                if (!result.Succeeded)
                {
                    Result.Data = new { Success = false };
                    return Result;
                }
                return Result;
            }
        }
    }
}