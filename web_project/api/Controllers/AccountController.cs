using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser>userManager,ITokenService tokenService,SignInManager<AppUser> signInManager)
        {
            _userManager=userManager;
            _tokenService=tokenService;
            _signInManager=signInManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO logindto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName==logindto.UserName.ToLower());
            if(user==null)
                return Unauthorized("Invalid username");
            var result =  await _signInManager.CheckPasswordSignInAsync(user,logindto.Password,false);
            if(!result.Succeeded)
            return Unauthorized("Username not found or password incorrect");
            return Ok(
                new NewUserDto
                {
                    userName=user.UserName,
                    Email=user.Email,
                    Token=_tokenService.CreateToken(user)
                }
            );
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDtO registerDTO)
        {
            try
            {
                if(!ModelState.IsValid)
                return BadRequest(ModelState);
                var AppUser=new AppUser
                {
                    UserName =registerDTO.Username,
                    Email=registerDTO.Email,
                };
                var createdUser = await _userManager.CreateAsync(AppUser,registerDTO.Password);
                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(AppUser,"User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto{
                                userName=AppUser.UserName,
                                Email=AppUser.Email,
                                Token=_tokenService.CreateToken(AppUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500,roleResult.Errors);
                    }
                }
                else
                return StatusCode(500,createdUser.Errors);
            }
            catch(Exception e)
            {
                return StatusCode(500,e);
            }
        }
    }
}