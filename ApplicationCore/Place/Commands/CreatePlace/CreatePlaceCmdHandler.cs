using ApplicationCore.BaseClasses;
using ApplicationCore.Location.Commands;
using ApplicationCore.Location.Queries;
using ApplicationCore.Place.Queries.GetPlaceByNameOrAddress;
using Domain;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
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

        var location = await Mediator.Send(new GetLocationQuery
        {
            Id = cmd.model.LocationId,
            Address = cmd.model.Address,
            Latitude = cmd.model.Latitude,
            Longitude = cmd.model.Longitude,
        });

        if (location is null)
        {
            location = await Mediator.Send(new AddLocationCmd
            {
                Model = new CreateLocationModel(
                    cmd.model.Address,
                    cmd.model.Latitude,
                    cmd.model.Longitude,
                    cmd.model.City,
                    cmd.model.Region,
                    cmd.model.PostalCode,
                    cmd.model.Country)
            });
        }

        var newPlace = new Domain.Entities.Place
        {
            CreatedBy = "iustin",
            ModifiedBy = "iustin",
            Name = cmd.model.Name,
            Description = cmd.model.Description,
            LocationId = location.Id,
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
