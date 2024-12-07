using Microsoft.EntityFrameworkCore;

namespace TCSA.WebAPI.FlightsData2.Data;
public class FlightsDbContext2 : DbContext
{
    public FlightsDbContext2(DbContextOptions options) : base(options)
    {

    }

    public DbSet<FlightsDbContext2> Flights { get; set; }
}
