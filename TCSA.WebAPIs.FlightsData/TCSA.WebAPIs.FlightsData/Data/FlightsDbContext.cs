using Microsoft.EntityFrameworkCore;
namespace TCSA.WebAPIs.FlightsData.Data;

public class FlightsDbContext : DbContext
{
    public FlightsDbContext(DbContextOptions options) : base(options)
    {

    }
}

