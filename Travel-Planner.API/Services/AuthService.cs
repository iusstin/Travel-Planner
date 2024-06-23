using ApplicationCore.User.Commands.Register;
using ApplicationCore.User.Queries.GetUser;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Travel_Planner.API.Services;

public class AuthService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IJwtUtils _jwtUtils;
    private readonly IValidator<RegisterCmd> _validator;

    public AuthService(
        IMediator mediator, 
        UserManager<User> userManager, 
        IJwtUtils jwtUtils, 
        IMapper mapper, 
        IValidator<RegisterCmd> validator)
    {
        _mediator = mediator;
        _userManager = userManager;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task RegisterUser(RegisterRequestModel model, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(model);
        var cmd = new RegisterCmd
        {
            model = user,
            Password = model.Password,
        };
        var validation = await _validator.ValidateAsync(cmd, cancellationToken);
        if (!validation.IsValid)
            throw new BadRequestException(validation.ToString());

        await _mediator.Send(cmd, cancellationToken);
    }

    public async Task<UserModel> LoginWithPassword(LoginRequestModel model, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserByEmailCmd { Email = model.Email }, cancellationToken);
        if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            throw new UnauthorizedAccessException("Invalid email or password");

        string token = _jwtUtils.GenerateToken(user);
        var userModel = new UserModel(user.Id, user.UserName, user.Email, token);
        return userModel;
    }
}
