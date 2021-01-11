using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DataAccess.Models;
using BussinesLogic.Helpers;
using Domain;
using System.Threading.Tasks;

namespace BussinesLogic.Authentication
{
    public class UserAuthService : IUserAuthService
    {
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork unit;
        private List<User> _users;

        public UserAuthService(IOptions<AppSettings> appSettings, IUnitOfWork unit)
        {
            _appSettings = appSettings.Value;
            this.unit = unit;
            _users = unit.Users.GetAllSync();
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public User GetById(int id) 
        {
            var user = _users.FirstOrDefault(x => x.ID == id);
            return user.WithoutPassword();
        }
    }
}