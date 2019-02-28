using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateDemo.Entities.Domain;

namespace NHibernateDemo
{
    public class UserServeris
    {
        static NHibernate.ISessionFactory SessionFactory = ISessionFactory.SessionFactory;

        public static IList<User> GetAll()
        { 
            using (var session = SessionFactory.OpenSession())
            {
                IList<User> list = session.CreateCriteria<User>().List<User>();
                return list;
            }
        }
  
        public static User GetById(int id)
        { 
            using (var session = SessionFactory.OpenSession())
            {
                //Get方法立即查询数据库，如果对象不存在，返回null
                User customer = session.Get<User>(id);
                return customer;
            }
        }


         public static int Insert(User customer)
         {
             using (var session = SessionFactory.OpenSession())
             {
                 var identifier = session.Save(customer);
                 session.Flush();
                 return Convert.ToInt32(identifier);
             }
         }

         public static void Update(User customer)
         {
             using (var session = SessionFactory.OpenSession())
             {
                //session.SaveOrUpdate(customer);  如果被调用的Customer对象在数据库里不存在（新记录），则插入新记录，否则修改该记录

                session.Update(customer, customer.Id);

                session.Flush(); //增删改操作完成之后需要调用session.Flush()方法，将对象持久化写入数据库。如果不调用此方法，方法结束后修改记录不能写入到数据库
            }
         }

         public static void Delete(int id)
         {
             using (var session = SessionFactory.OpenSession())
             {
                //当需要得到实体对象，但是不需要访问对象属性的时候，宜使用Load方法.在NHibernate里称为Lazy Loding（延迟加载）
                var customer = session.Load<User>(id);
                 session.Delete(customer);
                 session.Flush();
             }
         }

    }
}
