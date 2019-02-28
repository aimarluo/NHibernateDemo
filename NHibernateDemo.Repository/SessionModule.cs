using System.Configuration;
using System.Web;


namespace NHibernateDemo.Repository
{
    public class SessionModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            if (ConfigurationManager.ConnectionStrings["defaultDb"] != null
                && ConfigurationManager.ConnectionStrings["defaultDb"].ConnectionString != "")
            {
                SessionManager.ConnectionString = ConfigurationManager.ConnectionStrings["defaultDb"].ConnectionString;
            }
            context.EndRequest += (sender, e) => SessionManager.CloseSession();
        }

        public void Dispose()
        {
        }
    }
}
