using Microsoft.AspNetCore.Mvc;
using TCSA.WebAPI.FlightsData2.Services;

namespace TCSA.WebAPI.FlightsData2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;
    public FlightsController(IFlightService flightService)
    {
        _flightService = flightService;
    }
}