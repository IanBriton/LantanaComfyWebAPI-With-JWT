using LantanaComfyAPI.Dto;
using LantanaComfyAPI.Dto.OtherEntities;
using LantanaComfyAPI.Dto.OtherObjects;
using LantanaComfyAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LantanaComfyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        //Route for seeding my roles to the database.
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var seedRoles = await _authService.SeedRolesAsync();
            return Ok(seedRoles);
        }

        //Route for registering users
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var registerResults = await _authService.RegisterAsync(registerDto);
            if (registerResults.IsSucceeded)
                return Ok(registerResults);

            return BadRequest(registerResults);
        }

        //Route for Login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var loginResult = await _authService.LoginAsync(loginDto);

            if (loginResult.IsSucceeded)
                return Ok(loginResult);
            return Unauthorized(loginResult);
        }



        //Route for logging out 
        [HttpPost]
        [Route("logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok("Successfully logged out.");
        }

        //Route for adding roles to users
        [HttpPost]
        [Route("make-admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
           var operationResult = await _authService.MakeAdminAsync(updatePermissionDto);

            if(operationResult.IsSucceeded)
                return Ok(operationResult);

            return BadRequest(operationResult);

        }


        //Route for making user owner
        [HttpPost]
        [Route("make-owner")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> MakeOwner([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeOwnerAsync(updatePermissionDto);

            if (operationResult.IsSucceeded)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }
    }
}
