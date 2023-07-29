using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnapShop.Form.Model;
using SnapShop.Framework.Authentication;
using SnapShop.Models;

namespace SnapShop.Controllers.Api
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        private readonly IUserManager _userManager;

        public AuthController(IAuthentication authentication, IUserManager userManager)
        {
            _userManager = userManager;
            _authentication = authentication;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login model)
        {
            string token;
            try
            {
                token = _authentication.Login(model.Username, model.Password);
            }
            catch (UnauthorizedAccessException)
            {
                return BadRequest("Invalid username or password.");
            }
            return Ok(new { token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Registration model)
        {
            User user = new User()
            {
                Username = model.Username,
                Email = model.Email,
            };
            string token;
            try
            {
                token = await _authentication.Register(user, model.Password);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("whoAmI")]
        public ActionResult<User> WhoAmI()
        {
            return Ok(_userManager.GetUser());
        }
    }
}
