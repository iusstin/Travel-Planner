using ApplicationCore.BaseClasses;
using Domain.Interfaces;
using MediatR;

namespace ApplicationCore.Place.Queries.GetAllPlaces;

public class GetAllPlacesQueryHandler
    : BaseQueryHandler<GetAllPlacesQuery, IEnumerable<Domain.Entities.Place>>
{
    private readonly IPlaceRepository _placeRepository;

    public GetAllPlacesQueryHandler(IMediator mediator, IPlaceRepository placeRepository)
        : base(mediator)
    {
        _placeRepository = placeRepository;
    }

    public override async Task<IEnumerable<Domain.Entities.Place>> Handle(GetAllPlacesQuery cmd, CancellationToken cancellationToken)
    {
        var places = await _placeRepository.GetAllPlaces(cancellationToken);
        return places;
    }
}
