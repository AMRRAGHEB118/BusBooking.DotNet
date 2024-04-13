using System.ComponentModel.DataAnnotations;

namespace BusBooking.DotNet.Models
{
    public class TravelerAppointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}