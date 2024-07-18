using ApplicationCore.BaseClasses;
using Domain.Interfaces;
using MediatR;

namespace ApplicationCore.User.Queries.GetUser;

public class GetUserByEmailCmdHandler : BaseCommandHandler<GetUserByEmailCmd, Domain.Entities.User?>
{
    private readonly IUserRepository _userRepository;
    public GetUserByEmailCmdHandler(IMediator mediator, IUserRepository userRepository)
        : base(mediator)
    {
        _userRepository = userRepository;
    }
    public override async Task<Domain.Entities.User?> Handle(GetUserByEmailCmd request, CancellationToken cancellationToken)
    {
        var result  = await _userRepository.GetByExpressionAsync(
            u => u.Email == request.Email, 
            cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return result.FirstOrDefault();
    }
}
 