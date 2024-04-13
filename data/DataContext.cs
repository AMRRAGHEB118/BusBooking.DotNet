using Microsoft.EntityFrameworkCore;
using BusBooking.DotNet.Models;

namespace BusBooking.DotNet.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }
    }
}