using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using API.DTOs;

namespace API.Controllers
{
  
    //[Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembersDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<MembersDto>>(users);
            return usersToReturn.ToList();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MembersDto>> GetUser(int id)
        {
            //var user = await _userRepository.GetUserByIdAsync(id);
            //var userToReturn = _mapper.Map<MembersDto>(user);
            //return userToReturn;
            return await _userRepository.GetMemberDtoAsync(id);
        }
    }
}
