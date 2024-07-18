using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ApplicationCore;
using Travel_Planner.API.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var CorsAllowedOrigins = "_CORSAllowedOrigins";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsAllowedOrigins, builder =>
    {
        builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
var connection = builder.Configuration.GetConnectionString("AppDbContextAzure");
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(connection, builder => builder.EnableRetryOnFailure()));
builder.Services.AddIdentityCore<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<AuthService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplicationCore()
    .AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseMiddleware<JwtMiddleware>();

app.UseCors(CorsAllowedOrigins);

app.MapControllers();

app.Run();
