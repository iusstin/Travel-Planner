using ApplicationCore.BaseClasses;
using System.Runtime.Serialization;

namespace ApplicationCore.User.Queries.GetUser;

[DataContract]
public class GetUserByEmailCmd : BaseCommand<Domain.Entities.User?>
{
    [DataMember]
    public string Email { get; set; }
}
