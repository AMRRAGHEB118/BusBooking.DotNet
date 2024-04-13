using System.ComponentModel.DataAnnotations;

namespace BusBooking.DotNet.Models
{
    public class BusDestination
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string DestinationName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}