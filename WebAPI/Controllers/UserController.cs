using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
                 _userService = userService;
        }


        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteById(int id)
        {
            _userService.DeleteById(id);
            return Ok();
        }


        [HttpGet("GetUsers")]
        public IActionResult GetAll()

        {

            var users = _userService.GetUsers()
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.FirstName,
                Surname = u.LastName,
                UserName = u.Email,
                OperationClaimId = u.OperationClaimId,
            })
            .ToList();

            return Ok(users);

        }


        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()

        {

            return Ok("Welcome guys");

        }


    }
}
