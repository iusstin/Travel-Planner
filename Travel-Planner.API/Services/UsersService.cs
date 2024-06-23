using ApplicationCore.User.Queries.GetUsers;
using MediatR;

namespace Travel_Planner.API.Services;

public class UsersService
{
    private readonly IMediator _mediator;

    public UsersService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<Domain.Entities.User>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _mediator.Send(new GetUsersQuery(), cancellationToken);
        return users ?? Enumerable.Empty<Domain.Entities.User>();
    }
}
