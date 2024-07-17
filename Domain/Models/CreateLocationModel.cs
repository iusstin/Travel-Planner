namespace Domain.Models;

public record CreateLocationModel(
    string Address, 
    string? Latitude, 
    string? Longitude,
    string? City,
    string? Region,
    string? PostalCode,
    string Country);
