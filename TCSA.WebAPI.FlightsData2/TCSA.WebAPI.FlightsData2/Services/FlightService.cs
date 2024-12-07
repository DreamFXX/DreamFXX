using TCSA.WebAPI.FlightsData2.Models;
using TCSA.WebAPI.FlightsData2.Data;

namespace TCSA.WebAPI.FlightsData2.Services;

public interface IFlightService
{
    public List<Flight> GetAllFlights();
    public Flight? GetFlightById(int id);
    public Flight CreateFlight(Flight flight);
    public Flight UpdateFlight(int id, Flight updatedFlight);
    public string? DeleteFlights(int id);
}

public class FlightService : IFlightService
{
    private readonly FlightsDbContext2 _dbContext;

    public FlightService(FlightsDbContext2 dbContext)
    {
        _dbContext = dbContext;
    }
}