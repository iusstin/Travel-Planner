using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Travel_Planner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        private readonly IJwtUtils _jwtUtils;
        public BaseController(IJwtUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
        }

        public string GetConnectedUserId()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault().Split(' ').Last();
            var id = _jwtUtils.ValidateToken(token);
            return id;
        }
    }
}
