using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly SignInManager<WebUser> _signInManager;
        public AccountController(UserManager<WebUser> accountManager, ITokenService tokenService, SignInManager<WebUser> signInManager)
        {
            _userManager = accountManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
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
                    UserName = newUser.UserName,
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
                    UserName = creatingUser.UserName,
                    Email = creatingUser.Email,
                    Token = _tokenService.CreateToken(creatingUser),
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDetails.UserName.ToLower());
            if (user == null)
            {
                return Unauthorized("Username not registered");
            }
            var signInObj = await _signInManager.CheckPasswordSignInAsync(user, loginDetails.Password, false);
            if (!signInObj.Succeeded)
            {
                return Unauthorized("Username or Password is incorrect");
            }
            var token = _tokenService.CreateToken(user);
            return Ok(token);

        }
    }
}
