using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StocksAPI.Dto.Account;
using StocksAPI.Models;

namespace StocksAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<WebUser> _userManager;
        public AccountController(UserManager<WebUser> accountManager)
        {
            _userManager = accountManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateAccount([FromBody] NewUserDto newUser)
        {
            try
            {
                if (!ModelState.IsValid || newUser.Password == null)
                {
                    return BadRequest(ModelState);
                }

                WebUser creatingUser = new()
                {
                    UserName = newUser.Name,
                    Email = newUser.Email,
                };

                var createdUser = await _userManager.CreateAsync(creatingUser, newUser.Password);
                if (!createdUser.Succeeded)
                {
                    return StatusCode(500, createdUser.Errors);
                }

                var createdRole = await _userManager.AddToRoleAsync(creatingUser, "user");
                if (!createdRole.Succeeded)
                {
                    return StatusCode(500, createdRole.Errors);
                }

                return Ok("User Created");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
