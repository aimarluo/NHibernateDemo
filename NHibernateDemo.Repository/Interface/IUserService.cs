using NHibernateDemo.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Repository.Interface
{
    public interface IUserService : IRepository<User>
    {
    }
}
