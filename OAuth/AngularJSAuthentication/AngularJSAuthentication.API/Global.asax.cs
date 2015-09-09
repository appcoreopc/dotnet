using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace AngularJSAuthentication.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<AuthContext>(null);
            // GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
