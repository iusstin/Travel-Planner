using ApplicationCore.User.Queries.GetUsers;
using AutoMapper;
using MediatR;

namespace Travel_Planner.API.Services;

public class UsersService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Domain.Entities.User>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _mediator.Send(new GetUsersQuery(), cancellationToken);
        return users ?? Enumerable.Empty<Domain.Entities.User>();
    }
}
