using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
namespace API.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context=context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register( string username, string password)
        {
            using var hmac=new HMACSHA512();
            var user=new AppUser
            {
                Username=username,
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt=hmac.Key
            };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }
    }
}