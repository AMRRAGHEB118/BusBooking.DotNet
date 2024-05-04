namespace BusBooking.DotNet.Dto
{
    public class DtoNewUser
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
    }
}