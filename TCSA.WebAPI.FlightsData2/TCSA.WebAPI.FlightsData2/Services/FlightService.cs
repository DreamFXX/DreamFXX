using TCSA.WebAPI.FlightsData2.Data;
using TCSA.WebAPI.FlightsData2.Models;

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
    private readonly FlightsDbContext2 Context;

    public FlightService(FlightsDbContext2 context)
    {
        Context = context;
    }

    public Flight CreateFlight(Flight flight)
    {
        var savedFlight = Context.Flights.Add(flight);
        Context.SaveChanges();
        return savedFlight.Entity;
    }

    public Flight UpdateFlight(int id, Flight flight)
    {
        var savedFlight = Context.Flights.Find(flight.Id);
        if (savedFlight == null)
        {
            return null;
        }
        Context.Entry(savedFlight).CurrentValues.SetValues(flight);
        Context.SaveChanges();

        return savedFlight;
    }

    public string? DeleteFlights(int id)
    {
        Flight savedFlight = Context.Flights.Find(id);

        if (savedFlight == null)
        {
            return null;
        }

        Context.Flights.Remove(savedFlight);

        return $"Successfully deleted flight with id: {id}";
    }

    public List<Flight> GetAllFlights()
    {
        return Context.Flights.ToList();
    }

    public Flight? GetFlightById(int id)
    {
        Flight savedFlight = Context.Flights.Find(id);

        return savedFlight == null ? null : savedFlight;
    }
}

