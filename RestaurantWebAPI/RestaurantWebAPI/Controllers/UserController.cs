using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        // POST: /api/User/Register
        public async Task<Object> PostApplicationUser([FromBody]ApplicationUserModel model)
        {
            var applicationUser = new ApplicationUser
            {
                Customer = model.Customer
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
            catch (Exception)
            {
                return BadRequest();
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
    }
}