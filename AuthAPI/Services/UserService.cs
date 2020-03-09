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
using Microsoft.Extensions.Options;
using AuthAPI.Repository;

namespace AuthAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }
        public User Authenticate(string email, string password)
        {

            var users = _userRepository.GetAll();
            
            var user = users.SingleOrDefault(x => x.Email == email && x.Password == password);
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
                Expires = DateTime.UtcNow.AddHours(4),
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