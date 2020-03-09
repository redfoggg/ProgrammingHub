using System.Collections.Generic;
using AuthAPI.Entities;

namespace AuthAPI.Services.Interfaces
{
    public interface IUserService
    {
         User Authenticate(string email, string password);
         IEnumerable<User> GetAll();
    }
}