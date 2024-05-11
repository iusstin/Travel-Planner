using ApplicationCore.BaseClasses;
using System.Runtime.Serialization;

namespace ApplicationCore.User.Queries.GetUsers;

[DataContract]
public class GetUsersQuery : BaseQuery<IEnumerable<Domain.Entities.User>>
{ }
