using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models.JSON;
using RestaurantWebAPI.Models.ServiceRequests;
using RestaurantWebAPI.Models.ServiceResponses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _context;

        public TokenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenerateJWTResponse GenerateJWT(GenerateJWTRequest request)
        {
            var response = new GenerateJWTResponse
            {
                IsSuccessful = false,
                Message = "",
                AuthenticatedModel = new AuthenticatedModel()
            };

            try
            {
                var user = _context.ApplicationUsers
                    .Include(u => u.Customer)
                    .FirstOrDefault(u => u.UserName == request.UserName);

                // Security key (Consider moving the key to a different place).
                string securityKey = "this_is_our_super_long_security_key_for_token_validation_project_2018_09_07$smesk.in";

                // Symmetric security key.
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                // Signing credentials.
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, "HS256");
                var secret = signingCredentials.Key.ToString();

                // Add claims.
                var claims = new List<Claim>();

                // Claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("FirstName", user.Customer.FirstName));
                claims.Add(new Claim("LastName", user.Customer.LastName));
                claims.Add(new Claim("Username", user.UserName));
                claims.Add(new Claim("EmailAddress", user.Customer.Email));
                claims.Add(new Claim("Birthday", user.Customer.DateOfBirth.ToString()));
                claims.Add(new Claim("PhoneNumber", user.Customer.PhoneNumber.ToString()));

                // Create access token.
                var accessToken = new JwtSecurityToken(
                        issuer: "smesk.in",
                        audience: "readers",
                        expires: DateTime.Now.AddSeconds(30),
                        signingCredentials: signingCredentials,
                        claims: claims
                );

                // Create refresh token.
                var refreshToken = new JwtSecurityToken(
                        issuer: "smesk.in",
                        audience: "readers",
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: signingCredentials,
                        claims: claims
                );

                response.AuthenticatedModel.AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
                response.AuthenticatedModel.RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
                response.AuthenticatedModel.Customer = user.Customer;
                response.Username = user.UserName;

                response.IsSuccessful = true;
                response.Message = "Successfully added JWT and associated information.";
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            // return the authenticated model.
            return response;
        }
    }
}
