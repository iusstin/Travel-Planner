using ApplicationCore.BaseClasses;
using Domain.Interfaces;
using MediatR;

namespace ApplicationCore.User.Queries.GetUserById;

public class GetUserByIdQueryHandler : BaseQueryHandler<GetUserByIdQuery, Domain.Entities.User?>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryHandler(IMediator mediator, IUserRepository userRepository)
        : base(mediator)
    {
        _userRepository = userRepository;
    }

    public override async Task<Domain.Entities.User?> Handle(GetUserByIdQuery cmd, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(cmd.Id.ToString());
        return user;
    }
}
