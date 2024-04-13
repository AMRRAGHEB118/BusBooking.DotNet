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
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}