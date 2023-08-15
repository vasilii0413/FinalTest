using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductsManagement.Features.User.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(UserRegister model);
        Task<UserManagerResponse> LoginUserAsync(UserLogin model);
    }

    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserManagerResponse> LoginUserAsync(UserLogin model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "User with this username not found",
                    IsSucces = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSucces = false
                };

            var claims = new[]
            {
                new Claim("Username",model.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSucces = true,
                ExpireDate = DateTime.Now.AddDays(1)
            };
        }

        public async Task<UserManagerResponse> RegisterUserAsync(UserRegister model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Register model is null");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSucces = false,
                };
            }

            var identityUser = new IdentityUser
            {
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created successfully",
                    IsSucces = true
                };
            }
            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSucces = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
