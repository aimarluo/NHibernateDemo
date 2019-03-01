using NHibernate;
using NHibernate.Criterion;
using NHibernateDemo.Entities.Domain;
using NHibernateDemo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Repository
{
    public class UserService : Repository<User>, IUserService
    {
        #region HQL语言查询
        /// <summary>
        /// HQL语言查询
        /// from 实体类类名
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns> 
        public IList<User> GetAboutUser(int userId)
        {
            return Session.CreateQuery("from User c where c.Id > :id").SetInt32("id", userId).List<User>();
        }
        #endregion

        #region NHibernate Criteria查询 一般情况下，在写简单查询的时候用Criteria查询，在写复杂查询的时候使用HQL或者Linq to NHibernate
        /// <summary>
        /// ISession.CreateCriteria方法返回持久化查询对象的ICriteria实例
        /// </summary>
        /// <returns></returns>
        public IList<User> QueryAllCriteria()
        {
            return Session.CreateCriteria<User>().List<User>();
        }

        /// <summary>
        /// CreateAlias方法创建别名
        /// </summary>
        /// <returns></returns>
        public IList<User> QueryAllCriteria2()
        {
            return Session.CreateCriteria<User>()
                .CreateAlias("Customer", "c").List<User>();
        }

        /// <summary>
        /// ICriteria.SetProjection方法将应用投影到查询中，Projections有很多静态方法生成Distinct、GroupBy、Max、Min、Avg、Sum等投影。
        /// </summary>
        /// <returns></returns>
        public IList<int> SelectIdCriteria()
        {
            IList<int> ids = Session.CreateCriteria(typeof(User))
                                .SetProjection(Projections.Distinct(Projections.ProjectionList().Add(Projections.Property("Id"))))
                                .List<int>();
            return ids;
        }

        /// <summary>
        /// ICriteria.Add方法用来添加where条件，Restrictions类提供了很多生成查询条件的API方法。

        //Restrictions.And方法和Restrictions.Or方法对应SQL的and和or条件，可以传入多个条件对象拼接and/or子句，可以灵活地使用括号进行分组。

        //示例中的Restrictions.Eq方法和Restrictions.Like方法是最常用的Restrictions方法API。

        //此外，Restrictions还提供Between、Ge、Gt、Le、Lt、In、IsNotNull、IsNull、Where等多个API方法，可以满足生成大部分查询条件。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<User> GetCustomerByNameCriteria(string name)
        {
            return Session.CreateCriteria<User>().Add(Restrictions.Eq("Name", name)).List<User>();
        }

        public IList<User> GetCustomersStartWithCriteria()
        {
            var list = Session.CreateCriteria<User>().Add(Restrictions.Like("Name", "J%")).List<User>();
            return list.ToList();
        }

        /// <summary>
        /// ICriteria.AddOrder方法添加order子句，传入Criteria.Order对象。Criteria.Order类构造函数传入两个参数，第一个参数表示对哪个属性进行排序，第二个字段表示是升序还是降序，true是升序，false是降序。
        /// </summary>
        /// <returns></returns>
        public IList<User> GetCustomersOrderByCriteria()
        {
            var list = Session.CreateCriteria<User>().AddOrder(new NHibernate.Criterion.Order("Name", true)).List<User>();
            return list.ToList();
        }

        /// <summary>
        /// 因为要对Order记录进行分组统计，所以使用CreateCriteria("Orders")创建与Orders的关联。
        /// </summary>
        /// <returns></returns>
        public IList<object[]> SelectOrderCountCriteria()
        {
            var query = Session.CreateCriteria(typeof(User)).CreateCriteria("Orders")
                    .SetProjection(Projections.ProjectionList()
                    .Add(Projections.GroupProperty("Id"))
                    .Add(Projections.RowCount()));
            return query.List<object[]>();
        }
        #endregion

        #region Linq to NHibernate基于.net Linq，非常灵活
        /// <summary>
        /// IQueryable对象是延迟加载的,ToList方法表示立即执行，得到IList<User>集合
        /// </summary>
        /// <returns></returns>
        public IList<User> QueryAllLinq()
        {
            return Session.Query<User>().ToList();
        }

        /// <summary>
        /// 创建别名
        /// </summary>
        /// <returns></returns>
        public IList<User> QueryAllLinq2()
        {
            return (from c in Session.Query<User>().ToList() select c).ToList();
        }

        /// <summary>
        /// 指定对象返回数组
        /// </summary>
        /// <returns></returns>
        public IList<int> SelectIdLinq()
        {
            var query = Session.Query<User>().Select(c => c.Id).Distinct().ToList();
            return query.ToList();
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<User> GetCustomerByNameLinq(string name)
        {
            return Session.Query<User>().Where(c => c.Name == name).ToList();
        }

        public IList<User> GetCustomersStartWithLinq()
        {
            var query = from c in Session.Query<User>() where c.Name.StartsWith("J") select c;
            return query.ToList();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        public IList<User> GetCustomersOrderByLinq()
        {
            var query = from c in Session.Query<User>() orderby c.Name ascending select c;
            return query.ToList();
        }

        public IList<User> GetCustomersOrderCountGreaterThanLinq()
        {
            var query = Session.Query<User>().Where(c => c.Orders.Count > 2);
            return query.ToList();
        }

        public IList<User> GetCustomersOrderDateGreatThanLinq(DateTime orderDate)
        {
            var query = Session.Query<User>().Where(c => c.Orders.Any(o => o.Ordered > orderDate));
            return query.ToList();
        }
        #endregion 

        #region NHibernate Query Over跟Criteria类似，不是很灵活，而且API很有限
        /// <summary>
        /// Query Over查询跟Criteria查询类似。首先创建IQueryOver对象，然后通过调用该对象的API函数，进行对象查询
        /// </summary>
        /// <returns></returns>
        public IList<User> QueryAllOver()
        {
            return Session.QueryOver<User>().List();
        }

        /// <summary>
        /// 指定对象，返回数组
        /// </summary>
        /// <returns></returns>
        public IList<int> SelectIdOver()
        {
            return Session.QueryOver<User>()
                .List<User>().Distinct().Select(c => c.Id).ToList();
        }

        public IList<User> GetCustomerByNameOver(string name)
        {
            return Session.QueryOver<User>().Where(c => c.Name == name).List();
        }

        public IList<User> GetCustomersStartWithOver()
        {
            //return Session.QueryOver<Customer>().Where(c => c.FirstName.StartsWith("J")).List();  //异常
            //return Session.QueryOver<Customer>().Where(c => c.FirstName == "J%").List();  //正确
            //return Session.QueryOver<Customer>().Where(Restrictions.On<Customer>(c => c.FirstName).IsLike("J%")).List();  //正确
            return Session.QueryOver<User>().Where(Restrictions.Like("Name", "J%")).List();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        public IList<User> GetCustomersOrderByOver()
        {
            return Session.QueryOver<User>().OrderBy(c => c.Name).Desc().List();
        }

        //public IList<OrderCount> SelectOrderCountOver()
        //   {
        //       var query = Session.QueryOver<Customer>()
        //           .JoinQueryOver<Demo.XML.Entities.Domain.Order>(o => o.Orders)    //关联查询，与Order对象关联，默认是inner join，可以调用重载方法指定join方式
        //           .Select(Projections.GroupProperty("Id"), Projections.RowCount()) //分组查询
        //           .TransformUsing(Transformers.AliasToBean<OrderCount>());         //将结果投影到OrderCount对象
        //       return query.List<OrderCount>();
        //   }

        /// <summary>
        /// 查询所有订单数量大于2的客户信息
        /// </summary>
        /// <returns></returns>
        //public IList<User> GetCustomersOrderCountGreaterThanOver()
        //{
        //    //分组查询
        //    var query = Session.QueryOver<User>()
        //        .JoinQueryOver<Order>(o => o.Orders)
        //        .Select(Projections.GroupProperty("Id"), Projections.RowCount());
        //    IList<object[]> groups = query.List<object[]>();
        //    //得到订单数大于2的Customer.Id
        //    IList<int> ids = groups.Where(g => (int)g[1] > 2).Select(g => (int)g[0]).ToList();
        //    //条件查询
        //    return Session.QueryOver<User>()
        //               .Where(Restrictions.In("Id", ids.ToArray<int>()))
        //               .List<User>();
        //}

        /// <summary>
        /// 查询在指定日期到当前时间内有下订单的客户信息
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        //public IList<User> GetCustomersOrderDateGreatThanOver(DateTime orderDate)
        //{
        //    var query = Session.QueryOver<User>()
        //        .JoinQueryOver<Order>(o => o.Orders)    //关联查询
        //        .Where(o => o.Ordered > orderDate);                              //查询条件
        //    return query.List<User>();
        //}


        #endregion 

        #region SQL Query（原生SQL查询）
        /*
 首先创建调用ISession.CreateSQLQuery方法，传入原生的SQL语句，生成ISQLQuery对象，这是使用SQL Query的第一步。

    ISQLQuery.AddEntity方法传入类型参数，将查询结果映射到User类。

    ISQLQuery.List方法立即执行，返回查询结果。
     */
        public IList<User> GetAllSQL()
        {
            return Session.CreateSQLQuery("select * from Customer").AddEntity(typeof(User)).List<User>();
        }

        public IList<User> GetAllSQL2()
        {
            return Session.CreateSQLQuery("select * from Customer as c").AddEntity(typeof(User)).List<User>();
        }


        /// <summary>
        /// ISQLQuery.AddScalar方法将查询语句的列映射到.net数据类型，传入的第一个参数是列名，第二个参数是类型，NHibernateUtil类中的公开了很多静态实例对象表示映射数据类型，这里使用Int32
        /// </summary>
        /// <returns></returns>
        public IList<int> SelectIdSQL()
        {
            return Session.CreateSQLQuery("select distinct c.Id from Customer c")
                .AddScalar("Id", NHibernateUtil.Int32)
                .List<int>();
        }

        /// <summary>
        /// SQL Query的查询参数传递跟HQL参数传递是一样的。而且这里是使用原生的SQL语句，比HQL更方便。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<User> GetCustomerByNameSQL(string name)
        {
            return Session.CreateSQLQuery("select * from User where Name = :name")
                .AddEntity(typeof(User))
                .SetString("name", name)
                .List<User>();
        }

        public IList<User> GetCustomersStartWithSQL()
        {
            return Session.CreateSQLQuery("select * from User where FirstName like 'J%'")
                .AddEntity(typeof(User))
                .List<User>();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        public IList<User> GetCustomersOrderBySQL()
        {
            return Session.CreateSQLQuery("select * from User c order by c.Name")
                .AddEntity(typeof(User))
                .List<User>();
        }


        /// <summary>
        /// 按分组查询客户Id及客户关联的订单数量
        /// </summary>
        /// <returns></returns>
        public IList<object[]> SelectOrderCountSQL()
        {
            return Session.CreateSQLQuery("select c.Id, count(*) as OrderCount from User c inner join [Order] o on c.Id=o.CustomerId group by c.Id")
                .AddScalar("Id", NHibernateUtil.Int32)
                .AddScalar("OrderCount", NHibernateUtil.Int32)
                .List<object[]>();
        }

        /// <summary>
        /// 查询所有订单数量大于2的客户信息
        /// </summary>
        /// <returns></returns>
        public IList<User> GetCustomersOrderCountGreaterThanSQL()
        {
            string sql = @"select * from Customer
      where Id in
      (
          select c.Id from Customer c inner join [Order] o on c.Id = o.CustomerId
          group by c.Id
          having count(*) > 2
      )";
            return Session.CreateSQLQuery(sql)
                .AddEntity(typeof(User))
                .List<User>();
        }

        /// <summary>
        /// 查询在指定日期到当前时间内有下订单的客户信息
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        public IList<User> GetCustomersOrderDateGreatThanSQL(DateTime orderDate)
        {
            return Session.CreateSQLQuery("select distinct c.* from User c inner join [Order] o on c.Id=o.CustomerId where o.Ordered > :orderDate")
                .AddEntity(typeof(User))
                .SetDateTime("orderDate", orderDate)
                .List<User>();
        }

        #endregion





    }
}
