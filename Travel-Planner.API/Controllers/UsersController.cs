using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Planner.API.Services;

namespace Travel_Planner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Domain.Entities.User>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _usersService.GetAllUsersAsync(cancellationToken);
            return Ok(result);
        }
    }
}
