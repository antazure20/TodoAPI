using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.DomainEntities;

namespace Todo.Domain.RepositoryInterfaces
{
    public interface IUserRepository : IGenericRepository<UserDomain>
    {
    }
}
