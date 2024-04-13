using System.ComponentModel.DataAnnotations;


namespace BusBooking.DotNet.Models
{
    public class AppointmentRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TravelerAppointmentId { get; set; }
        [Required]
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}