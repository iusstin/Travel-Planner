using Domain.Entities;

namespace Domain.Interfaces;

public interface IJwtUtils
{
    public string GenerateToken(User user);
    public string? ValidateToken(string token);
}
