using ApplicationCore.BaseClasses;

namespace ApplicationCore.Place.Queries.GetPlaceByNameOrAddress;

public class GetPlaceByNameOrAddressQuery : BaseQuery<IEnumerable<Domain.Entities.Place>>
{
    public string? Name { get; set; }
    public string? Address { get; set; }
}
