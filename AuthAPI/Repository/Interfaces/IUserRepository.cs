using System.Collections.Generic;
using AuthAPI.Entities;
using AuthAPI.Repository.Base;

namespace AuthAPI.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
         IList<User> Users();
         User UserByUsername(string userName);
    }
}