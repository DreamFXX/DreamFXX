using System.ComponentModel.DataAnnotations;

namespace WorkerShifts.API.Models;

public class Worker
{
    [Key]
    public int WorkerId { get; set; }
    [Required]
    public string WorkerName { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
