using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

using NLog;

using Schematrix.Data;

namespace Form2WebApp
{
    public class Global : HttpApplication
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SqlServerHelper.ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            if (exception == null)
                return;

            Exception ex = exception;

            do
            {
                if (ex.Message != null)
                {
                    if (log != null)
                        log.Error(ex.Message.Replace("\r\n", " "));
                }

                ex = ex.InnerException;

            } while (ex != null);

            log.Error(string.Format("Stack Trace:\r\n{0}", exception));

            Server.ClearError();

            Response.Redirect("~/Pages/Default.aspx");
        }
    }
}
