using ApplicationCore.BaseClasses;
using System.Runtime.Serialization;

namespace ApplicationCore.User.Queries.GetUserById;

[DataContract]
public class GetUserByIdQuery : BaseQuery<Domain.Entities.User?>
{
    [DataMember]
    public long Id { get; set; }
}
