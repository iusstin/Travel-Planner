using ApplicationCore.BaseClasses;
using ApplicationCore.Place.Queries.GetPlaceByNameOrAddress;
using Domain;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace ApplicationCore.Place.Commands.CreatePlace;

public class CreatePlaceCmdHandler : BaseCommandHandler<CreatePlaceCmd, Domain.Entities.Place>
{
    private readonly IPlaceRepository _placeRepository;
    public CreatePlaceCmdHandler(IMediator mediator, IPlaceRepository placeRepository)
        : base(mediator)
    {
        _placeRepository = placeRepository;
    }

    public override async Task<Domain.Entities.Place> Handle(CreatePlaceCmd cmd, CancellationToken cancellationToken)
    {
        var existingPlace = await Mediator.Send(new GetPlaceByNameOrAddressQuery
        {
            Name = cmd.model.Name,
            Address = cmd.model.Address,
        }, cancellationToken);

        if (existingPlace.Any())
            throw new DuplicateEntityException("Entity exists");

        var newPlace = new Domain.Entities.Place
        {
            CreatedBy = "iustin",
            ModifiedBy = "iustin",
            Name = cmd.model.Name,
            Description = cmd.model.Description,
            Address = cmd.model.Address,
            City = cmd.model.City,
            Country = cmd.model.Country,
            Latitude = cmd.model.Latitude,
            Longitude = cmd.model.Longitude,
            Link = cmd.model.Link,
            PhoneNumber = cmd.model.PhoneNumber,
            Image = cmd.model.Image,
            Cost = cmd.model.Cost,
            AverageDuration = cmd.model.AverageDuration,
            Currency = (Currency)Enum.Parse(typeof(Currency), cmd.model.Currency),
        };

        await _placeRepository.CreatePlace(newPlace, cancellationToken);
        return newPlace;
    }
}
