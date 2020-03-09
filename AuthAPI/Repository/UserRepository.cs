using System.Collections.Generic;
using System.Linq;
using AuthAPI.Data;
using AuthAPI.Entities;
using AuthAPI.Repository.Base;

namespace AuthAPI.Repository.Interfaces
{
    public class UserRepository : RepositoryBase<User> , IUserRepository
    {

        public UserRepository(AuthContext context) : base (context) {}

        /// <summary>
        /// Select a single one User filtred by Username
        /// </summary>
        /// <param value="userName">userName</param>
        /// <returns>User</returns>
        public User UserByUsername(string userName)
        {
            return this.GetSingle(u => u.UserName == userName);
        }

        /// <summary>
        /// List all users currently in DB
        /// </summary>
        /// <returns>IList<User></returns>
        public IList<User> Users()
        {
            return this.GetAll().ToList();
        }
    }
}