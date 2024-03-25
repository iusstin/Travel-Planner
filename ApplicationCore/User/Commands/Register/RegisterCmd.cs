using ApplicationCore.BaseClasses;
using MediatR;
using System.Runtime.Serialization;

namespace ApplicationCore.User.Commands.Register;

[DataContract]
public class RegisterCmd : BaseCommand<Unit>
{
    [DataMember]
    public Domain.Entities.User model { get; set; }
    [DataMember]
    public string Password { get; set; }
}
