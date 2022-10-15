
using Ecommerce.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class CBRoleManager : RoleManager<IdentityRole>
    {
        public CBRoleManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {
        }
        public static CBRoleManager Create(IdentityFactoryOptions<CBRoleManager> options, IOwinContext context)
        {
            return new CBRoleManager(new RoleStore<IdentityRole>(context.Get<CBContext>()));
        }
    }
}
