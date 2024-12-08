using Microsoft.AspNetCore.Mvc;
using TCSA.WebAPI.FlightsData2.Models;
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

    [HttpGet]
    public ActionResult<List<Flight>> GetAllFlights()
    {
        return Ok(_flightService.GetAllFlights());
    }

    [HttpGet("{id}")]
    public ActionResult<Flight> GetFlightById(int id)
    {
        var result = _flightService.GetFlightById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Flight> CreateFlight(Flight flight)
    {
        return Ok(_flightService.CreateFlight(flight));
    }

    [HttpPut("{id}")]
    public ActionResult<Flight> UpdateFlight(Flight flight)
    {
        var result = _flightService.GetFlightById(flight.Id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<string?> DeleteFlights(int id)
    {
        var result = _flightService.GetFlightById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}