using System.Collections.Generic;
using AuthAPI.Entities;
using AuthAPI.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AuthAPI.Services.Interfaces;
using System.Text;
using System.Security.Claims;
using System;
using AuthAPI.Repository.Interfaces;
using System.Linq;

namespace AuthAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User Authenticate(string username, string password)
        {

            var users = _userRepository.GetAll();

            var user = users.SingleOrDefault(x => x.UserName == username && x.Password == password);
            if(user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPasswords();
        }

        public IEnumerable<User> GetAll()
        {
            var users = _userRepository.GetAll();
            return users.WithoutPasswords();
        }


    }
}