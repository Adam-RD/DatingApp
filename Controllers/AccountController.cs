using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService): BaseApiController
{
    [HttpPost("register")] // account/register

    public async Task<ActionResult<UserDto>> Register (RegisterDTO registerDTO)
    {
        if (await UserExists(registerDTO.UserName) ) return BadRequest("Username ya existe");

        return Ok();
    /*
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDTO.UserName.ToLower(),
            PassWordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password)),
            PassWordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)

        };

        */


    }

    [HttpPost("Login")]

    public async Task<ActionResult<UserDto>> Login(LoginDTO loginDTO)
    {
    var user = await context.Users.FirstOrDefaultAsync(x =>
     x.UserName == loginDTO.UserName.ToLower());

     if (user == null) return Unauthorized("Usuario Invalido");

     using var hmac = new HMACSHA512 (user.PassWordSalt);

     var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

     for (int i = 0; i < computeHash.Length; i++)
     {
        if (computeHash [i] != user.PassWordHash[i]) return Unauthorized("Contraseña invalidad");
     }
        
        return new UserDto 
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
                    
        };
    }

    private async Task<bool> UserExists (string username){
        return await context.Users.AnyAsync(x =>x.UserName.ToLower() == username.ToLower());
    }

}
