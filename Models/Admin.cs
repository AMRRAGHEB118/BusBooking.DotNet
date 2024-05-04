using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BusBooking.DotNet.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Password { get; set; }
    }
}