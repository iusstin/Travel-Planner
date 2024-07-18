using ApplicationCore.BaseClasses;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Trip.Queries;

public class GetTripsByOwnerQuery : BaseQuery<IEnumerable<Domain.Entities.Trip>>
{
    public string OwnerId { get; set; }
}

public class GetTripsByOwnerQueryHandler : BaseQueryHandler<GetTripsByOwnerQuery, IEnumerable<Domain.Entities.Trip>>
{
    private readonly ITripRepository _tripRepository;

    public GetTripsByOwnerQueryHandler(IMediator mediator, ITripRepository tripRepository) 
        : base(mediator)
    {
        _tripRepository = tripRepository;
    }

    public override async Task<IEnumerable<Domain.Entities.Trip>> Handle(GetTripsByOwnerQuery cmd, CancellationToken cancellationToken)
    {
        var ownerTrips = await _tripRepository.GetByExpression(
            filter: t => t.TripCreatorId == cmd.OwnerId,
            include: t => t.Include(tm => tm.Mates),
            cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return ownerTrips ?? Enumerable.Empty<Domain.Entities.Trip>();
    }
}
