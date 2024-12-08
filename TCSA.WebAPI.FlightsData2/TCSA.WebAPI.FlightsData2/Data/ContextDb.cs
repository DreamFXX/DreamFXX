using Microsoft.EntityFrameworkCore;
using TCSA.WebAPI.FlightsData.Models;

namespace TCSA.WebAPI.FlightsData.Data;
public class FlightsDbContext2 : DbContext
{
    public FlightsDbContext2(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Flight> Flights { get; set; }
}
