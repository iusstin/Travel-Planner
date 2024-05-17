using ApplicationCore.Place.Commands.CreatePlace;
using ApplicationCore.Place.Queries.GetAllPlaces;
using AutoMapper;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Travel_Planner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlacesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PlacesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Place>>> GetAll(CancellationToken cancellationToken)
    {
        var places = await _mediator.Send(new GetAllPlacesQuery(), cancellationToken);
        var result = places ?? Enumerable.Empty<Place>();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePlaceModel model, CancellationToken cancellationToken)
    {
        var place = await _mediator.Send(new CreatePlaceCmd { model = model }, cancellationToken );
        return Ok(place);
    }
}
