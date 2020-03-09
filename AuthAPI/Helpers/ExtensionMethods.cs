using System.Collections.Generic;
using System.Linq;
using AuthAPI.Entities;

namespace AuthAPI.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPasswords());
        }

        public static User WithoutPasswords(this User user)
        {
            user.Password = null;
            return user;
        }
    }
}