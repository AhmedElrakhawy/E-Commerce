using Ecommerce.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.ViewModel
{
    public class UserRolesViewModel
    {
        public List<IdentityRole> AvailableRoles { get; set; }
        public List<CBUser> Users { get; set; }
    }
    public class ManagePostUserRolesViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public IdentityRole RoleName { get; set; }
        public List<string> Roles { get; set; }
        public bool Selected { get; set; }
    }
    public class ChangeRoleViewModel
    {
        public string ID { get; set; }
        public string RoleName { get; set; }
    }
    public class UserProfileViewModel
    {
        public CBUser User { get; set; }
    }
    public class UserUpdateViewModel
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}