using Microsoft.EntityFrameworkCore;

public class FlightsDbsContext : DbContext
{
    public FlightsDbsContext(DbContextOptions options) : base(options)
    {

    }
}

