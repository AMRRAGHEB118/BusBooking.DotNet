using System.ComponentModel.DataAnnotations;

namespace BusBooking.DotNet.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BusDestinationId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int Capacity { get; set; }
        public int Booked { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}