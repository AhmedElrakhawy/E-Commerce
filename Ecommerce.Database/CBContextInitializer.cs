using Ecommerce.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Database
{
    public class CBContextInitializer : CreateDatabaseIfNotExists<CBContext>
    {
        protected override void Seed(CBContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
        }

        public void SeedRoles(CBContext context)
        {
            List<IdentityRole> CBRoles = new List<IdentityRole>();
            CBRoles.Add(new IdentityRole() { Name = "Administrator" });
            CBRoles.Add(new IdentityRole() { Name = "User" });
            CBRoles.Add(new IdentityRole() { Name = "Moderator" });
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            foreach (IdentityRole role in CBRoles)
            {
                if (!roleManager.RoleExists(role.Name))
                {
                    var result = roleManager.Create(role);
                    if (result.Succeeded)
                    {
                        continue;
                    }
                }
            }
        }

        public void SeedUsers(CBContext context)
        {
            var usersStore = new UserStore<CBUser>(context);
            var userManager = new UserManager<CBUser>(usersStore);
            CBUser admin = new CBUser();
            admin.Email = "admin@gmail.com";
            admin.UserName = "admin";
            var Passward = "admin123";

            if (userManager.FindByEmail(admin.Email) == null)
            {
                var result = userManager.Create(admin, Passward);

                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, "Administrator");
                    userManager.AddToRole(admin.Id, "User");
                    userManager.AddToRole(admin.Id, "Moderator");
                }
            }
        }
    }
}
