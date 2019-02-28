using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace NHibernateDemo
{
    public class ISessionFactory
    {
        private static NHibernate.ISessionFactory _sessionFactory;

        public static NHibernate.ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var cfg = new Configuration();

                    //cfg.DataBaseIntegration(x =>
                    //{
                    //    x.ConnectionString = "Server=localhost;Database=test;User ID=root;Password=mysql5.6";
                    //    x.Driver<MySqlDataDriver>();
                    //    x.Dialect<MySQL5Dialect>();
                    //    x.LogSqlInConsole = true;
                    //});
                    //cfg.AddAssembly(Assembly.GetExecutingAssembly());

                    cfg.Configure(); //调用xml配置
                    _sessionFactory = cfg.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }  

    }
}
