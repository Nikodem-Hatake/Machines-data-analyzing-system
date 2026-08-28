using System.ComponentModel.DataAnnotations;

namespace Domain.Tests.Models
{
    public class AggregatedMachineDatasSelector
    {
        [Required]
        public DateTime? StartDate { get; set; } = DateTime.Now.Date;

        [Required]
        public TimeSpan? Time { get; set; } = TimeSpan.Zero;

        [Required]
        [Range(1, 10, ErrorMessage = "Number should be in range from {1} to {2}.")]
        public int HowManyDatesForward { get; set; }
    }
}