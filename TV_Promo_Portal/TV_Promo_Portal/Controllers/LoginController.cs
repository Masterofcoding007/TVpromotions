using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TV_Promo_Portal.Models;

namespace TV_Promo_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        TV_PromoContext db = new TV_PromoContext();
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Usermaster login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login, false);
            if (user != null)
            {
                var tokenString = GenerateToken(user);
                response = Ok(new { token = tokenString, userData = user });
            }
            return response;
        }

        private Usermaster AuthenticateUser(Usermaster login, bool IsRegister)
        {
            if (IsRegister)
            {
                db.Usermasters.Add(login);
                db.SaveChanges();
                return login;
            }
            else
            {
                if (db.Usermasters.Any(x => x.Username == login.Username && x.Password == login.Password))
                {
                    return db.Usermasters.Where(x => x.Username == login.Username && x.Password == login.Password).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }

        }

        private string GenerateToken(Usermaster login)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(_config["jwt:Issuer"],
            //    _config["jwt:Audience"],
            //    null,
            //    expires: DateTime.Now.AddMinutes(120),
            //    signingCredentials: credentials);
            //return new JwtSecurityTokenHandler().WriteToken(token);

            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var token = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, login.UserId.ToString())
                    }),
                    Expires = DateTime.Now.AddMinutes(120),
                    SigningCredentials = credentials
                };
                var TokenHandler = new JwtSecurityTokenHandler();
                var tokenGenerated = TokenHandler.CreateToken(token);
                return TokenHandler.WriteToken(tokenGenerated).ToString();
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(Usermaster login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login, true);
            if (user != null)
            {
                var tokenString = GenerateToken(user);
                response = Ok(new { token = tokenString, userData = user });
            }
            return response;
        }
    }
}
