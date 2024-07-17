using ApplicationCore.BaseClasses;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System.Runtime.Serialization;

namespace ApplicationCore.Location.Commands;

[DataContract]
public class AddLocationCmd : BaseCommand<Domain.Entities.Location>
{
    [DataMember]
    public CreateLocationModel Model { get; set; }
}

public class AddLocationCmdHandler : BaseCommandHandler<AddLocationCmd, Domain.Entities.Location>
{
    private readonly ILocationRepo _locationRepo;

    public AddLocationCmdHandler(IMediator mediator, ILocationRepo locationRepo) : base(mediator)
    {
        _locationRepo = locationRepo;
    }

    public override async Task<Domain.Entities.Location> Handle(AddLocationCmd cmd, CancellationToken cancellationToken)
    {
        var location = new Domain.Entities.Location
        {
            Address = cmd.Model.Address,
            Latitude = cmd.Model.Latitude,
            Longitude = cmd.Model.Longitude,
            City = cmd.Model.City,
            Region = cmd.Model.Region,
            PostalCode = cmd.Model.PostalCode,
            Country = cmd.Model.Country,
            CreatedBy = "iustin",
            ModifiedBy = "iustin",
        };

        await _locationRepo.Add(location, cancellationToken);
        return location;
    }
}

