using Ecommerce.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class UserService
    {
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
    }
}
