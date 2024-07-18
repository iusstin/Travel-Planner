using ApplicationCore.BaseClasses;
using Domain.Interfaces;
using MediatR;

namespace ApplicationCore.Location.Queries;

public class GetLocationQuery : BaseQuery<Domain.Entities.Location?>
{
    public long? Id { get; set; } = null;
    public string? Address { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
}

public class GetLocationQueryHandler : BaseQueryHandler<GetLocationQuery, Domain.Entities.Location?>
{
    private readonly ILocationRepository _locationRepo;

    public GetLocationQueryHandler(IMediator mediator, ILocationRepository locationRepo)
        : base(mediator)
    {
        _locationRepo = locationRepo;
    }

    public override async Task<Domain.Entities.Location?> Handle(GetLocationQuery cmd, CancellationToken cancellationToken)
    {
        if (cmd.Id is not null)
            return await _locationRepo.GetById((long)cmd.Id).ConfigureAwait(false);

        var locations = await _locationRepo.GetByExpressionAsync(
            filter: l => l.Address == cmd.Address ||
            l.Latitude == cmd.Latitude && l.Longitude == cmd.Longitude)
            .ConfigureAwait(false);

        return locations.FirstOrDefault();
    }
}
