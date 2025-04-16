﻿using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Obada_Shop.API.DTOs.Requests;
using Obada_Shop.API.Model;

namespace Obada_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser>signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task <IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var applicationUser = registerRequest.Adapt<ApplicationUser>();
            var result = await userManager.CreateAsync(applicationUser, registerRequest.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(applicationUser, false);
                return NoContent();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task <IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var applicationUser = await userManager.FindByEmailAsync(loginRequest.Email);
            if (applicationUser != null)
            {
                var result = await userManager.CheckPasswordAsync(applicationUser, loginRequest.Password);
                if (result)
                {
                    await signInManager.SignInAsync(applicationUser,loginRequest.RememberMe);
                    return NoContent();
                }
            }
            return BadRequest(new { message = "Invalid email or password!!" });
        }

        [HttpGet("logout")]
        public async Task <IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }
        [Authorize]
        [HttpPost("changePassword")]
        public async Task <IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var applicationUser = await userManager.GetUserAsync(User);
            if (applicationUser != null)
            {
                var result = await userManager.ChangePasswordAsync(applicationUser,changePasswordRequest.OldPassword,changePasswordRequest.NewPassword);
                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest(new { message = "Invalid data!!" });
        }
    }
}
