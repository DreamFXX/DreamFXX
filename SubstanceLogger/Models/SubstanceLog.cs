using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SubstanceLogger.Models
{
    public class SubstanceLog
    {
        public int Id { get; set; }

        [Required]
        public int SubstanceId { get; set; }
        public Substance? Substance { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        public IdentityUser? User { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Log Date")]
        public DateTime LogDate { get; set; } = DateTime.Now;

        [Required]
        [Range(0.01, 1000)]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [StringLength(50)]
        [Display(Name = "Unit")]
        public string Unit { get; set; } = "mg";

        [StringLength(500)]
        [Display(Name = "Notes")]
        public string? Notes { get; set; }
    }
}
