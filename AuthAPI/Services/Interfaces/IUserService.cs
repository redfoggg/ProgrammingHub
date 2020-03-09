using System.Collections.Generic;
using AuthAPI.Entities;

namespace AuthAPI.Services.Interfaces
{
    public interface IUserService
    {
         User Authenticate(string username, string password);
         IEnumerable<User> GetAll();
    }
}