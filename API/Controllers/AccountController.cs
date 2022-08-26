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
using API.DTO;
using API.Interface;

namespace API.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context,ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context=context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await ChekcUsernameExists(registerDto.Username)) return BadRequest("User Name Already Exists!");

            using var hmac=new HMACSHA512();
            var user=new AppUser
            {
                Username=registerDto.Username.ToLower(),
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                PasswordSalt=hmac.Key,
                Gender= registerDto.Gender,
                DateOfBirth=registerDto.DateOfBirth,
                KnownAs=registerDto.KnownAs,
                Created=registerDto.Created,
                LastActive=registerDto.LastActive,
                Introduction=registerDto.Introduction,
                LookingFor=registerDto.LookingFor,
                Interests=registerDto.Interests,
                City=registerDto.City,
                Country=registerDto.Country,
                Photos=registerDto.Photos
            };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username=user.Username,
                Token=_tokenService.CreateToken(user)
            };
        }

        private async Task<bool> ChekcUsernameExists(string username)
        {
            return await _context.Users.AnyAsync(x=>x.Username==username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            var user = await _context.Users
            .SingleOrDefaultAsync(x=>x.Username==loginDto.Username.ToLower());

            if(user==null) return Unauthorized("Invalid UserName!!");

            using var hmac= new HMACSHA512(user.PasswordSalt);

            var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i=0;i<computedHash.Length;i++)
            {
                if(computedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid Password!!");

            }
            return new UserDto
            {
                Username=user.Username,
                Token=_tokenService.CreateToken(user)
            };

        }
    }
}