using Application.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace OneAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController
        (
            ILogger<UserController> logger,
            IUserService userService
        )
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            _logger.LogInformation("Executing list users");
            return Ok(_userService.ListUsers(10, 10));
        }

        [HttpGet("{userId:int}")]
        public IActionResult GetUserById([FromRoute]int userId)
        {
            _logger.LogInformation("Executing get user by id");
            return Ok(_userService.GetById(userId));
        }
    }
}
