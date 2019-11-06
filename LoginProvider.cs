
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ZeonEcommerce.Models;

namespace ZeonEcommerce
{
    public class LoginProvider : OAuthAuthorizationServerProvider
    {
        public Suppliers Login(string email, string password)
        {
            var db = new ECommerceContext();
            var usr = db.Suppliers.Where(x => x.Email == email && x.Password == password && x.IsActive == true).ToList();

            if (usr.Count > 0)
            {
                //ok
                return usr.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return base.ValidateClientAuthentication(context);
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usr = Login(context.UserName, Crypto.Hash(context.Password));
            if (usr == null)
            {
                //error
                context.SetError("ivalid_grant", "Istifadeci melumatlarinda sehv var");
            }
            else
            {
                //ok
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("UserName", context.UserName));
                identity.AddClaim(new Claim("Password", Crypto.Hash(context.Password)));
                identity.AddClaim(new Claim("UserID", usr.SuppliersId.ToString()));

                context.Validated(identity);
            }
            return base.GrantResourceOwnerCredentials(context);
        }
    }
}