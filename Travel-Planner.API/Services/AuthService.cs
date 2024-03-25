using ApplicationCore.User.Commands.Register;
using ApplicationCore.User.Queries.GetUser;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Travel_Planner.API.Services;

public class AuthService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(IMediator mediator, UserManager<User> userManager, IJwtUtils jwtUtils, IMapper mapper)
    {
        _mediator = mediator;
        _userManager = userManager;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    public async Task RegisterUser(RegisterRequestModel model, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(model);
        var cmd = new RegisterCmd
        {
            model = user,
            Password = model.Password,
        };

        await _mediator.Send(cmd);
    }

    public async Task<UserModel> LoginWithPassword(LoginRequestModel model, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserByEmailCmd { Email = model.Email });
        if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            throw new ArgumentException("Invalid email or password");

        string token = _jwtUtils.GenerateToken(user);
        var userModel = new UserModel(long.Parse(user.Id), user.UserName, user.Email, token);
        return userModel;
    }
}
