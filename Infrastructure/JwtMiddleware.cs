using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
        var userId = jwtUtils.ValidateToken(token);
        if (userId != null)
            context.Items["User"] = await userRepository.GetById(userId);

        await _next(context);
    }
}
