using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        public RegisterController(IConfiguration config,AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(SignUp register)
        {
            try
            {
                if (_context.UserTable.Where(u => u.Email== register.Email).FirstOrDefault()!=null)
                {
                    return BadRequest("!!User Exists.");
                }
                User user = new User();
                user.Email = register.Email;
                user.Password = register.Password;
                user.Name = register.Name;
                user.PanNumber = register.PanNumber;
                user.AccNumber = register.AccNumber;
                user.BankName = register.BankName;
              
                _context.UserTable.Add(user);
                _context.SaveChanges();
                return Ok("Sign Up successfully.");
                
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(SignIn data)
        {
            Console.WriteLine(data.Email);
            var userAvail = _context.UserTable.Where(u => u.Email == data.Email && u.Password == data.Password).FirstOrDefault();
            Console.WriteLine(userAvail);
            if (userAvail!=null)
            {
                return Ok(new JwtServices(_config).GenerateToken(
                    userAvail.id.ToString(),
                    userAvail.Email,
                    userAvail.Name));
            }
            return BadRequest("Failure...");
        }
    }
}
