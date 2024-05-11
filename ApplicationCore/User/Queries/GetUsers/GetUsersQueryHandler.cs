using ApplicationCore.BaseClasses;
using Domain.Interfaces;
using MediatR;

namespace ApplicationCore.User.Queries.GetUsers;

public class GetUsersQueryHandler : BaseQueryHandler<GetUsersQuery, IEnumerable<Domain.Entities.User>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IMediator mediator, IUserRepository userRepository)
        : base(mediator)
    {
        _userRepository = userRepository;
    }

    public override async Task<IEnumerable<Domain.Entities.User>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsers(cancellationToken);
        return users;
    }
}
