using ApplicationCore.BaseClasses;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.User.Commands.Register;

public class RegisterCmdHandler : BaseCommandHandler<RegisterCmd, Unit>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IUserRepository _userRepository;

    public RegisterCmdHandler(
        IMediator mediator, 
        IUserRepository userRepository, 
        UserManager<Domain.Entities.User> userManager) : base(mediator)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public override async Task<Unit> Handle(RegisterCmd request, CancellationToken cancellationToken)
    {
        var result = await _userManager.CreateAsync(request.model, request.Password);
        await _userRepository.Add(request.model, cancellationToken);
        return Unit.Value;
    }
}
