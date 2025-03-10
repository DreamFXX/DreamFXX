namespace WorkerShifts.API.Models;

public class Shift
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Description { get; set; }
    public TimeSpan Duration => EndTime - StartTime;

    // Navigation property
    public Worker? Worker { get; set; }
}
