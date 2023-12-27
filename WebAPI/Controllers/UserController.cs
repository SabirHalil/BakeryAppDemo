using Business.Abstract;

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
            //var result = _userService.GetUsers();

            //return Ok(result);

            var users = _userService.GetUsers()
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.FirstName,
                Surname = u.LastName,
                UserName = u.Email,
                OperationClaim = u.OperationClaim,
            })
            .ToList();

            return Ok(users);


        }
        public class UserDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string UserName { get; set; }
            public string OperationClaim { get; set; }
            

        }

    }
}
