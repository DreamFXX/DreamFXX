using Microsoft.EntityFrameworkCore;
using TCSA.WebAPI.FlightsData2.Models;

namespace TCSA.WebAPI.FlightsData2.Data;
public class FlightsDbContext2 : DbContext
{
    public FlightsDbContext2(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Flight> Flights { get; set; }
}
