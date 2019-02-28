using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernateDemo.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
             var list = UserServeris.GetAll();

            //var single = UserServeris.GetById(1);

            //var r = UserServeris.Insert(new User { Name = "赵六", Age = 50 });

            //UserServeris.Update(new User { Id = 5,  Age = 15, Name = "小李5" });

            //UserServeris.Delete(5);

            //var session = ISessionFactory.SessionFactory.OpenSession();
            //bool contains = false;
            ////临时态
            // User customer = CreateCustomer();
            //            contains = session.Contains(customer); //持久化状态的对象被session管理
            //            Console.WriteLine("Is Persistent：{0}", contains);

            // //临时态 -> 持久态
            // session.Save(customer);
            //             contains = session.Contains(customer);
            //             Console.WriteLine("Is Persistent：{0}", contains);

            // //持久态 -> 游离态
            // session.Evict(customer); //或 session.Clear();
            //             contains = session.Contains(customer);
            //             Console.WriteLine("Is Persistent：{0}", contains);

            // //游离态 -> 持久态
            // customer.LastName = "Chen";
            //             session.SaveOrUpdate(customer);
            //             session.Flush(); //将修改马上写入数据库
            //             contains = session.Contains(customer);
            //             Console.WriteLine("Is Persistent：{0}", contains);

            // //持久态 -> 临时态
            // session.Delete(customer);
            //             session.Flush(); //将修改马上写入数据库
            //             contains = session.Contains(customer);
            //             Console.WriteLine("Is Persistent：{0}", contains);


            //在同一个session里执行两次相同记录的查询
            //using (var session = ISessionFactory.SessionFactory.OpenSession())
            //{
            //    var customer1 = session.Get<User>(1); 

            //    if (session.Contains(customer1))
            //    {
            //        session.Evict(customer1);
            //    }


            //    var customer2 = session.Get<User>(1);
            //}

            //var single1 = UserServeris.GetById(1);

            //UserServeris.Update(new User { Name = "张三2", Age = 50, Id = 1 });

            //var single2 = UserServeris.GetById(1);


            //在两个session里两次查询相同对象
            //using (var session = ISessionFactory.SessionFactory.OpenSession())
            //{
            //    var customer = session.Get<User>(1);
            //}

            //using (var session = ISessionFactory.SessionFactory.OpenSession())
            //{
            //    var customer = session.Get<User>(1);
            //}


            //using (var session = ISessionFactory.SessionFactory.OpenSession())
            //{
            //    IList<User> customers = session.CreateQuery("from User where id > 1")
            //    .SetCacheable(true)
            //    .List<User>();
            //    Console.WriteLine("list count: {0}", customers.Count);
            //}

            //using (var session = ISessionFactory.SessionFactory.OpenSession())
            //{
            //    IList<User> customers = session.CreateQuery("from User where id > 1")
            //    .SetCacheable(true)
            //    .List<User>();
            //    Console.WriteLine("list count: {0}", customers.Count);
            //} 

            //using (var session = ISessionFactory.SessionFactory.OpenSession())
            //{
            //    var customer1 = session.Get<User>(1);
            //    var customer2 = session.Get<User>(1);

            //    customer1.Name = "Chen";
            //    customer2.Name = "Liu";
            //    session.Update(customer1);
            //    session.Flush();
            //    session.Update(customer2);
            //    session.Flush();
            //}

            //using (var session = ISessionFactory.SessionFactory.OpenSession())
            //{
            //    var customer1 = session.Get<User>(1);
            //    var customer2 = session.Get<User>(1);

            //    session.Delete(customer1);
            //    session.Delete(customer2);
            //    session.Flush();
            // }

            #region 事务机制

            //using (var session = ISessionFactory.SessionFactory.OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        try
            //        {
            //            // some date operaton codes here

            //            var customer1 = session.Get<User>(3);
            //            var customer2 = session.Get<User>(4);

            //            customer1.Name = "cc";
            //            customer2.Name = "dd";

            //            session.Update(customer1);
            //            session.Flush();
            //            session.Update(customer2);
            //            session.Flush();

            //            transaction.Commit();
            //        }
            //        catch (HibernateException e)
            //        {
            //            transaction.Rollback();  // or log exception
            //        }
            //    }
            //}

            #endregion 

            #region 如何映射值对象
            //using (var session = ISessionFactory.SessionFactory.OpenSession())
            //{
            //    var customer = new User
            //    { 
            //        Address = new Address
            //        {
            //            City = "宁波",
            //            Country = "中国",
            //            Province = "浙江",
            //            Street = "高新区"
            //        },
            //        Age = 18,
            //        Name = "大神"
            //    };
            //    object custoemrId = session.Save(customer);
            //    session.Flush();
            //}  
            #endregion





            Console.ReadLine();

        } 

    }
}

