using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Features.User.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductsManagement.Features.User
{
    [Route("api/Authorization")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private IUserService _userService;

        public AuthorizationController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        [SwaggerOperation(
        Summary = "Register User",
        Description = "This API will register a new user",
        OperationId = "RegisterUser",
        Tags = new[] { "Authorization" })]
        public async Task<IActionResult> RegisterAsync([FromBody]UserRegister model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if(result.IsSucces)
                    return Ok(result);
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        [SwaggerOperation(
        Summary = "Login User",
        Description = "This API will login the registered user",
        OperationId = "LoginUser",
        Tags = new[] { "Authorization" })]
        public async Task<IActionResult> LoginAsync([FromBody]UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSucces)
                    return Ok(result);
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }



    }
}
