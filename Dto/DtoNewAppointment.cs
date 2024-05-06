using System.ComponentModel.DataAnnotations;

namespace BusBooking.DotNet.Dto
{
    public class DtoNewAppointment
    {
        public int BusDestinationId { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        [Range(5, 50)]
        public int Capacity { get; set; }
    }
}