using ApplicationCore.BaseClasses;
using Domain.Models;
using System.Runtime.Serialization;

namespace ApplicationCore.Place.Commands.CreatePlace;

[DataContract]
public class CreatePlaceCmd : BaseCommand<Domain.Entities.Place>
{
    [DataMember]
    public CreatePlaceModel model { get; set; }
}
