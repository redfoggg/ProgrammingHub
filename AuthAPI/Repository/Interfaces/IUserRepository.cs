using System.Collections.Generic;
using AuthAPI.Entities;

namespace AuthAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
         IList<User> Users();
         User UserByUsername(string userName);
    }
}