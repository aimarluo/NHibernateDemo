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

    }
}
