﻿using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Planner.API.Services;

namespace Travel_Planner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AuthService _authService;

        public AuthController(AuthService authService, IJwtUtils jwtUtils, ILogger<AuthController> logger) : base(jwtUtils)
        {
            _authService = authService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterWithPassword([FromBody] RegisterRequestModel request, CancellationToken cancellationToken)
        {
            await _authService.RegisterUser(request, cancellationToken);
            _logger.LogInformation($"Welcome {request.UserName}");
            return Ok("Registration successful");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> AuthenticateWithPassword([FromBody] LoginRequestModel model, CancellationToken cancellationToken)
        {
            var response = await _authService.LoginWithPassword(model, cancellationToken);
            return Ok(response);
        }
    }
}
