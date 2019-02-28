using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System;
using System.Web;

namespace NHibernateDemo.Repository
{
    public class SessionManager
    {
        private static ISessionFactory SessionFactory { get; set; }

        public static string ConnectionString { get; set; }

        private static ISessionFactory GetFactory<T>() where T : ICurrentSessionContext
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(x => {
 
                x.LogSqlInConsole = true; 

                if (!string.IsNullOrEmpty(ConnectionString)
                    && ConnectionString.Trim() != "")
                {
                    x.ConnectionString = ConnectionString;
                }
            });

            cfg.Configure().CurrentSessionContext<T>();
            return cfg.BuildSessionFactory();
        }

        /// <summary>
        /// Gets the current session.
        /// </summary>
        public static ISession GetCurrentSession()
        {
            if (SessionFactory == null)
            {
                SessionFactory = HttpContext.Current != null ? GetFactory<WebSessionContext>() : GetFactory<ThreadStaticSessionContext>();
            }

            if (CurrentSessionContext.HasBind(SessionFactory))
            {
                return SessionFactory.GetCurrentSession();
            }

            var session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            return session;
        }

        /// <summary>
        /// Closes the session.
        /// </summary>
        public static void CloseSession()
        {
            if (SessionFactory != null && CurrentSessionContext.HasBind(SessionFactory))
            {
                var session = CurrentSessionContext.Unbind(SessionFactory);
                if (session != null && session.IsOpen)
                {
                    session.Close();
                }
            }
        }

        /// <summary>
        /// Begin the transaction.
        /// </summary>
        public static void BeginTransaction()
        {
            var session = GetCurrentSession();

            try
            {
                if (session != null && session.IsOpen)
                {
                    var transaction = session.BeginTransaction();
                }
            }
            catch (Exception e)
            {
                CloseSession();
                throw e;
            }
        }

        /// <summary>
        /// Commit the transaction
        /// </summary>
        public static void CommitTransatcion()
        {
            var session = GetCurrentSession();
            ITransaction transaction = null;

            try
            {
                if (session != null && session.IsOpen)
                {
                    transaction = session.Transaction;
                    if (transaction != null
                        && transaction.IsActive
                        && !transaction.WasCommitted
                        && !transaction.WasRolledBack)
                    {
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    CloseSession();
                }
                throw e;
            }
        }

        /// <summary>
        /// Rollback the transaction
        /// </summary>
        public static void RollbackTransaction()
        {
            var session = GetCurrentSession();
            ITransaction transaction = null;
            try
            {
                if (session != null && session.IsOpen)
                {
                    transaction = session.Transaction;
                    if (transaction != null
                        && transaction.IsActive
                        && !transaction.WasCommitted
                        && !transaction.WasRolledBack)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                    CloseSession();
                }
                throw e;
            }
        }
    }
}
