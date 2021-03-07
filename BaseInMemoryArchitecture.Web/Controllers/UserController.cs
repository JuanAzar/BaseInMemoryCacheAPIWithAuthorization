using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BaseInMemoryArchitecture.BusinessLogic.Contracts;
using BaseInMemoryArchitecture.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BaseInMemoryArchitecture.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController(IConfiguration configuration, IMapper mapper, IUserService userService) : base(configuration, mapper)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserCredentialsVM userCredentials)
        {
            var user = _userService.Login(userCredentials.Email, userCredentials.Password);

            if (user == null)
                return Unauthorized("Invalid Credentials");

            var userVM = _mapper.Map<UserVM>(user);

            var token = GenerateJWTToken(userVM);

            return Ok(new
            {
                token = token,
                userDetails = userVM
            });
        }

        private string GenerateJWTToken(UserVM user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var role = (user.UserTypeId == 1) ? "Admin" : "Client";

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, "Client"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
