using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
public class AccountController : BaseApiController
{
    private readonly DataContext _dataContext;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext dataContext, ITokenService tokenService)
    {
        _dataContext = dataContext;
        _tokenService = tokenService;
    }

    
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await CheckUserExists(registerDto.Username)) return BadRequest("Username taken!");

        using var rhs = new HMACSHA512();

        var userTobeAdded = new AppUser()
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = rhs.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordKey = rhs.Key
        };

        _dataContext.Users.Add(userTobeAdded);
        await _dataContext.SaveChangesAsync();

        return new UserDto
        {
            Username = userTobeAdded.UserName,
            Token = _tokenService.CreateToken(userTobeAdded)
        };
    }

    private async Task<bool> CheckUserExists(string username)
    {
        return await _dataContext.Users.AnyAsync(x => x.UserName == username.ToLower());
    }

  
    [HttpGet("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        if (user == null) return Unauthorized("User name invalid!");

        using var rsh = new HMACSHA512(user.PasswordKey);
        var computedHash = rsh.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < user.PasswordHash.Length; i++)
        {
            if (user.PasswordHash[i] != computedHash[i]) return Unauthorized("Invalid Password!");
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }
}
