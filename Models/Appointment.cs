using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BusBooking.DotNet.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(BusDestination))]
        public int BusDestinationId { get; set; }

        public BusDestination BusDestination { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int Capacity { get; set; }
        public int Booked { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}