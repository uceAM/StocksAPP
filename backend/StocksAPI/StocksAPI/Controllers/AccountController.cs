using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StocksAPI.Dto.Account;
using StocksAPI.Interfaces;
using StocksAPI.Models;

namespace StocksAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<WebUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<WebUser> accountManager, ITokenService tokenService)
        {
            _userManager = accountManager;
            _tokenService = tokenService;
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

                return Ok(new UserTokenDto()
                {
                    Username = creatingUser.UserName,
                    Email = creatingUser.Email,
                    Token = _tokenService.CreateToken(creatingUser),
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
