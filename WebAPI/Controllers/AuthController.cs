using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Claims;
using System.Xml.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(string userName,string password )
        {
        
            var userToLogin = _authService.Login(new UserForLoginDto { UserName= userName,Password= password });
            if (!userToLogin.Success)
            {
                return BadRequest();
                return NoContent();
                return BadRequest(userToLogin.Message);
            }


            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                LoginResponseDto loginResponseDto = new LoginResponseDto {

                 Id=   userToLogin.Data.Id,
                Name = userToLogin.Data.FirstName,
                   OperationClaimId = userToLogin.Data.OperationClaimId,
                   Token = result.Data.Token};
                return Ok(loginResponseDto);
               
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.UserName);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

    }
}
