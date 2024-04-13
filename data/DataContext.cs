using Microsoft.EntityFrameworkCore;
using BusBooking.DotNet.Models;

namespace BusBooking.DotNet.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<BusDestination> BusDestinations { get; set; }
        public DbSet<TravelerAppointment> TravelerAppointments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }
    }
}