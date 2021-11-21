using ShopNetCoreApi.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopNetCoreApi.Models.Entities;
using ShopNetCoreApi.Models.ViewModel;
using System.Security.Claims;

namespace ShopNetCoreApi.Data.Repositories
{
    public class UserRepositories : RepositoryBase<AppUser>, IUserRepositories
    {
        private readonly ShopDbContext _dbContext;
        public AppConfig AppConfig { get; private set; }
        public UserRepositories(ShopDbContext dbContext, IOptions<AppConfig> apOptions) : base(dbContext)
        {
            _dbContext = dbContext;
            AppConfig = apOptions.Value;
        }

        public AuthenticateResponse Authenticate(string userName, string passWord)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
                return null;
            var user = _dbContext.AppUsers.FirstOrDefault(x => x.UserName == userName);
            if (user == null) return null;
            var passWordUser = Convert.FromBase64String(user.PassWord);
            var passwordSalt = Convert.FromBase64String(user.PasswordSalt);
            if (!VerifyPasswordHash(passWord, passWordUser, passwordSalt))
                return null;
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, (string)token);
        }

        private object generateJwtToken(AppUser user)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(AppConfig.Expries)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        
            var token = tokenHandle.CreateToken(tokenDescriptor);
            return tokenHandle.WriteToken(token);
        }

        private bool VerifyPasswordHash(string passWord, byte[] passWordUser, byte[] passwordSalt)
        {
            if (passWord == null) throw new ArgumentNullException("password");
            if(passWordUser.Length != 64) throw new ArgumentNullException("Invalid length of password hash(64 bytes expected)","password");
            if (passwordSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passWord));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passWordUser[i]) return false;
                }
            }

            return true;
        }
    }
}
