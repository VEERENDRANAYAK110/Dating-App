using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using API.DTO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.Interface;
using AutoMapper;


namespace API.Controllers
{
    [Authorize]
    public class UsersController:BaseApiController
    {
        private readonly IUserRespository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRespository userRespository,IMapper mapper)
        {
            _userRepository= userRespository;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users=await _userRepository.GetMembersAsync();
            // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);

            // return _mapper.Map<MemberDto>(user);
        }
    }
}