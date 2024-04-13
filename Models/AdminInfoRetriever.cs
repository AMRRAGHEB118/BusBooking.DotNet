

namespace BusBooking.DotNet.Models
{
    public static class AdminInfoRetriever
    {
        public static void RetrieveAndValidateAdminInfo()
        {
            string name = Environment.GetEnvironmentVariable("ADMIN_NAME");
            string email = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
            string password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                throw new ApplicationException("Admin name or password or email not provided in environment variables.");
            }
        }
    }
}