﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models.Bodies;
using RestaurantWebAPI.Models.Entities;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Services;

namespace LogInWebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        // POST: /api/User/Register
        public async Task<Object> PostApplicationUser([FromBody]ApplicationUserModel model)
        {
            // Query on the customer.
            var customer = _context.Customers
                .Where(x => x.Id == model.CustomerId)
                .FirstOrDefault();

            if (customer != null)
            {
                var applicationUser = new ApplicationUser
                {
                    Customer = customer,
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                try
                {
                    var result = await _userManager.CreateAsync(applicationUser, model.Password);

                    if (result.Succeeded)
                    {
                        var request = new GenerateJWTRequest
                        {
                            UserName = model.UserName
                        };

                        var authenticatedModel = _tokenService.GenerateJWT(request);
                        return Ok(authenticatedModel);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }

            return NoContent();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        // POST: api/User/Login
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var request = new GenerateJWTRequest
                {
                    UserName = model.UserName
                };
                
                var authenticatedModel = _tokenService.GenerateJWT(request);
                return Ok(authenticatedModel);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        // PUT: api/User/ChangePassword
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordModel model)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    if (model.NewPassword == model.ConfirmNewPassword)
                    {
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);

                        var updateResult = await _userManager.UpdateAsync(user);

                        if (!updateResult.Succeeded)
                        {
                            return BadRequest("Password wasn't successfully updated.");
                        }
                        else if (updateResult.Succeeded)
                        {
                            return Ok($"Password for username: {model.UserName} was succesfully updated!");
                        }
                    }
                }
            }

            return NotFound();
        }

        [HttpPut("ForgetPassword")]
        // PUT: api/User/ChangePassword
        public async Task<IActionResult> ForgetPassword([FromBody]ForgetPasswordModel model)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                if (model.EmailAddress == user.Email.ToString() && model.PhoneNumber == user.PhoneNumber.ToString() && model.NewPassword == model.ConfirmNewPassword)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);

                    var updateResult = await _userManager.UpdateAsync(user);

                    if (!updateResult.Succeeded)
                    {
                        return BadRequest("Password wasn't successfully updated.");
                    }
                    else if (updateResult.Succeeded)
                    {
                        return Ok($"Password for username: {model.UserName} was succesfully updated!");
                    }
                }
            }

            return NotFound();
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(id);

            if (user != null)
            {
                return Ok(new { username = user.UserName });
            }
            else
            {
                return Ok(new { username = "" });
            }
        }
    }
}