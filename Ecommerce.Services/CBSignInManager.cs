using Ecommerce.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{

    public class CBSignInManager : SignInManager<CBUser, string>
    {
        public CBSignInManager(CBUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(CBUser user)
        {
            return user.GenerateUserIdentityAsync((CBUserManager)UserManager);
        }

        public static CBSignInManager Create(IdentityFactoryOptions<CBSignInManager> options, IOwinContext context)
        {
            return new CBSignInManager(context.GetUserManager<CBUserManager>(), context.Authentication);
        }
    }
}
