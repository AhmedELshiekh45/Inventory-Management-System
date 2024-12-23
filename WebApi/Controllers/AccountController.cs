using DataModels.Dtos;
using DataModels.Genral_Responce;
using DataModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repos.Unit_Of_Work;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private User user;

        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;
            this.user = new User();
        }

        [HttpPost("Register User")]
        public async Task<ActionResult<GenralResponce>> RegisterGust(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                user.UserName = registerDto.UserName;

                user.Email = registerDto.Email;
                user.PasswordHash = registerDto.Password;
                user.PhoneNumber = registerDto.PhoneNumber;
                var result = await _unitOfWork.UserManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    var found = await _unitOfWork.UserManager.AddToRoleAsync(user, "User");
                    if (found.Succeeded)
                    {

                        return Created(); //new GenralResponce { IsSuccess = true, Data = "Created Succesfuly" };
                    }
                    else
                    {
                        foreach (var item in found.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return new GenralResponce { Data = ModelState, IsSuccess = false };

        }



        [Authorize(Roles = "Admin")]
        [HttpPost("Register Admin")]
        public async Task<ActionResult<GenralResponce>> RegisterAdmin(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                user.UserName = registerDto.UserName;
                user.Email = registerDto.Email;
                user.PasswordHash = registerDto.Password;
                user.PhoneNumber = registerDto.PhoneNumber;
                var result = await _unitOfWork.UserManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    var found = await _unitOfWork.UserManager.AddToRoleAsync(user, "Admin");
                    var claim = new Claim("Role", "Admin");
                    await _unitOfWork.UserManager.AddClaimAsync(user, claim);
                    if (found.Succeeded)
                    {

                        return Created(); //new GenralResponce { IsSuccess = true, Data = "Created Succesfuly" };
                    }
                    else
                    {
                        foreach (var item in found.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return new GenralResponce { Data = ModelState, IsSuccess = false };

        }


        [Authorize(Roles = "Admin")]
        [HttpPost("Register Employee")]
        public async Task<ActionResult<GenralResponce>> RegisterEmployee(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                user.UserName = registerDto.UserName;
               
                user.Email = registerDto.Email;
                user.PasswordHash = registerDto.Password;
                user.PhoneNumber = registerDto.PhoneNumber;
                var result = await _unitOfWork.UserManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    var found = await _unitOfWork.UserManager.AddToRoleAsync(user, "Employee");
                    var claim = new Claim("Role", "Employee");
                    await _unitOfWork.UserManager.AddClaimAsync(user, claim);
                    if (found.Succeeded)
                    {

                        return Created(); //new GenralResponce { IsSuccess = true, Data = "Created Succesfuly" };
                    }
                    else
                    {
                        foreach (var item in found.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return new GenralResponce { Data = ModelState, IsSuccess = false };

        }



        [HttpPost("LogIn")]
        public async Task<ActionResult<GenralResponce>> LogIn(LogInDto logInDto)
        {
            if (ModelState.IsValid)
            {
                user = await _unitOfWork.UserManager.FindByEmailAsync(logInDto.Email);
                if (user != null)
                {
                    var found = await _unitOfWork.UserManager.CheckPasswordAsync(user, logInDto.Password);
                    if (found)
                    {
                        var token = await GenerateJwtToken(user);
                        var claims = await _unitOfWork.UserManager.GetClaimsAsync(user);
                        var claim = claims.First().Value;
                        if (claim == "Admin")
                        {

                            return new GenralResponce { IsSuccess = true, Data = new { Token = token, Message = $"Welcome Admin {User.Identity?.Name}" } };
                        }
                        else if (claim == "Employee")
                        {
                            return new GenralResponce { IsSuccess = true, Data = new { Token = token, Message = $"Welcom Employee {User.Identity?.Name} " } };

                        }
                        else
                        {
                            return new GenralResponce { IsSuccess = true, Data = new { Token = token, Message = $"Welcom Gust {User.Identity?.Name} " } };

                        }
                    }
                }
            }
            return Ok(ModelState);
        }



        private async Task<string> GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
           //     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var roles = await _unitOfWork.UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
