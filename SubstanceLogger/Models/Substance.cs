using System.ComponentModel.DataAnnotations;

namespace SubstanceLogger.Models
{
    public class Substance
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        // Navigation property
        public ICollection<SubstanceLog> Logs { get; set; } = new List<SubstanceLog>();
    }
}
