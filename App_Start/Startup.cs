using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(ZeonEcommerce.Startup))]
namespace ZeonEcommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.Configuration(app);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new LoginProvider()

            });
            //Yukarıda belirlemiş olduğumuz authorization ayarlarında çalışması için server'a ilgili OAuthAuthorizationServerOptions tipindeki nesneyi gönderiyoruz.


            //Authentication Type olarak Bearer Authentication'ı kullanacağımızı belirtiyoruz.
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            //Bearer Token, OAuth 2.0 ile gelen standartlaşmış bir token türüdür.
            //Herhangi bir kriptolu veriye ihtiyaç duyulmaksızın client tarafından token isteğinde bulunulur ve server belirli bir zamana sahip access_token üretir.
            //Bearer Token SSL güvenliğine dayanır.

        }
    }
}