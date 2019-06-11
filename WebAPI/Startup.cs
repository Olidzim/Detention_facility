using Detention_facility.Controllers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Detention_facility.Business;


[assembly: OwinStartup(typeof(Detention_facility.Startup))]

namespace Detention_facility
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            CustomLogging.Initialize(HttpContext.Current.Server.MapPath("~"));
            UnityConfig.RegisterTypes();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureOAuth(app, GlobalConfiguration.Configuration.DependencyResolver);          
        }
        public void ConfigureOAuth(IAppBuilder app, System.Web.Http.Dependencies.IDependencyResolver resolver)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationProvider((IAuthorizationService)resolver.GetService(typeof(AuthorizationService)))              
            };
                       
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

    }
}