using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Security.Claims;
using Travel_Planner.API.Services;

namespace Travel_Planner.API.Controllers;

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
        //var claims = new ConcurrentBag<Claim>()
        //{
        //    new(ClaimTypes.Email, response.Email),
        //    new(ClaimTypes.Name, response.UserName),
        //    new(ClaimTypes.UserData, response.Id)
        //};

        //var identity = new ClaimsIdentity(claims);
        //var principal = new ClaimsPrincipal(identity);
        //var properties = new AuthenticationProperties { IsPersistent = false };

        //SignIn(principal, properties);
        return Ok(response);
    }

    //[HttpGet]
    //public ActionResult<UserModel> Identify()
    //{
    //    var user = new UserModel(ClaimTypes.UserData, ClaimTypes.Name, "tosa");

    //}
}
