using ApplicationCore.BaseClasses;
using Domain.Interfaces;
using MediatR;

namespace ApplicationCore.Place.Queries.GetPlaceByNameOrAddress;

public class GetPlaceByNameOrAddressQueryHandler
    : BaseQueryHandler<GetPlaceByNameOrAddressQuery, IEnumerable<Domain.Entities.Place>>
{
    private readonly IPlaceRepository _placeRepository;

    public GetPlaceByNameOrAddressQueryHandler(IMediator mediator, IPlaceRepository placeRepository) 
        : base(mediator)
    {
        _placeRepository = placeRepository;
    }

    public override async Task<IEnumerable<Domain.Entities.Place>> Handle(GetPlaceByNameOrAddressQuery cmd, CancellationToken cancellationToken)
    {
        var places = await _placeRepository.GetByExpression(
            filter: p => p.Name == cmd.Name || p.Location.Address == cmd.Address, 
            cancellationToken: cancellationToken);

        return places ?? Enumerable.Empty<Domain.Entities.Place>();
    }
}
