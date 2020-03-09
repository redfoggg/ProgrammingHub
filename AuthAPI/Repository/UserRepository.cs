using System.Collections.Generic;
using System.Linq;
using AuthAPI.Data;
using AuthAPI.Entities;
using AuthAPI.Repository.Base;
using AuthAPI.Repository.Interfaces;

namespace AuthAPI.Repository
{
    public class UserRepository : RepositoryBase<User> , IUserRepository
    {

        public UserRepository(AuthContext context) : base (context) {}

        /// <summary>
        /// Select a single one User filtred by email
        /// </summary>
        /// <param value="email">email</param>
        /// <returns>User</returns>
        public User UserByEmail(string email)
        {
            return this.GetSingle(u => u.Email == email);
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