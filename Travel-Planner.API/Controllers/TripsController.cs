using ApplicationCore.Trip.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Travel_Planner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TripsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{ownerId}")]
    public async Task<ActionResult<IEnumerable<Trip>>> GetTripsByOwner(string ownerId)
    {
        var result = await _mediator.Send(new GetTripsByOwnerQuery { OwnerId = ownerId });
        return Ok(result);
    }
}
