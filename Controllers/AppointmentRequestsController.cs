using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusBooking.DotNet.data;
using BusBooking.DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BusBooking.DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentRequestsController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public AppointmentRequestsController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public class AppointmentRequestResponse
        {
            public int RequestId { get; set; }
            public required string RequestStatus { get; set; }
            public required string TravelerName { get; set; }
            public DateTime DateTime { get; set; }
            public int Capacity { get; set; }
            public int Booked { get; set; }
            public required string DestinationName { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentRequestResponse>>> Get()
        {
            var appointmentRequests = await (from appointmentRequestRecord in _dbContext.AppointmentRequests
                                            join travelerAppointment in _dbContext.TravelerAppointments on appointmentRequestRecord.TravelerAppointmentId equals travelerAppointment.Id
                                            join appointment in _dbContext.Appointments on travelerAppointment.AppointmentId equals appointment.Id
                                            join traveler in _dbContext.Users on travelerAppointment.UserId equals traveler.Id
                                            join busDestination in _dbContext.BusDestinations on appointment.BusDestinationId equals busDestination.Id
                                            select new AppointmentRequestResponse
                                            {
                                                RequestId = appointmentRequestRecord.Id,
                                                RequestStatus = appointmentRequestRecord.Status,
                                                TravelerName = traveler.UserName ?? "Unknown",
                                                DateTime = appointment.DateTime,
                                                Capacity = appointment.Capacity,
                                                Booked = appointment.Booked,
                                                DestinationName = busDestination.DestinationName
                                            }).ToListAsync();
            return appointmentRequests;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentRequestResponse>> Get(string id)
        {
            var appointmentRequest = await (from appointmentRequestRecord in _dbContext.AppointmentRequests
                                            join travelerAppointment in _dbContext.TravelerAppointments on appointmentRequestRecord.TravelerAppointmentId equals travelerAppointment.Id
                                            join appointment in _dbContext.Appointments on travelerAppointment.AppointmentId equals appointment.Id
                                            join traveler in _dbContext.Users on travelerAppointment.UserId equals traveler.Id
                                            join busDestination in _dbContext.BusDestinations on appointment.BusDestinationId equals busDestination.Id
                                            where traveler.Id == id
                                            select new AppointmentRequestResponse
                                            {
                                                RequestId = appointmentRequestRecord.Id,
                                                RequestStatus = appointmentRequestRecord.Status,
                                                TravelerName = traveler.UserName ?? "Unknown",
                                                DateTime = appointment.DateTime,
                                                Capacity = appointment.Capacity,
                                                Booked = appointment.Booked,
                                                DestinationName = busDestination.DestinationName
                                            }).ToListAsync();
            return Ok(appointmentRequest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<AppointmentRequestResponse>>> UpdateRequestStatus(int id, string status)
        {
            var dbRequest = await _dbContext.AppointmentRequests.FindAsync(id);
            if (dbRequest == null)
                return NotFound($"Request with Id = {id} not found");

            if (status.ToLower() == "approved") {
                var dbTravelerAppointment = await _dbContext.TravelerAppointments.FindAsync(dbRequest.TravelerAppointmentId);
                if (dbTravelerAppointment == null)
                    return BadRequest("Traveler Appointment not found");

                var dbAppointment = await _dbContext.Appointments.FindAsync(dbTravelerAppointment.AppointmentId);
                if (dbAppointment == null)
                    return BadRequest("Traveler or Appointment not found");

                if(dbAppointment.Booked < dbAppointment.Capacity) {
                    dbAppointment.Booked += 1;
                } else {
                    return BadRequest("No seats available");
                }
            }

            dbRequest.Status = status;
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.AppointmentRequests.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<AppointmentRequestResponse>>> Delete(int id)
        {
            var dbRequest = await _dbContext.AppointmentRequests.FindAsync(id);
            if (dbRequest == null)
                return NotFound($"Request with Id = {id} not found");

            _dbContext.AppointmentRequests.Remove(dbRequest);
            await _dbContext.SaveChangesAsync();
            var dbTravelerAppointment = await _dbContext.TravelerAppointments.FindAsync(dbRequest.TravelerAppointmentId);

            if (dbTravelerAppointment != null) 
                _dbContext.TravelerAppointments.Remove(dbTravelerAppointment);

            await _dbContext.SaveChangesAsync();
            return Ok("Request deleted successfully");
        }

        [HttpPost]
        public async Task<ActionResult<List<AppointmentRequestResponse>>> AddRequest(string id, int appointmentId)
        {
            var traveler = await _dbContext.Users.FindAsync(id);
            var appointment = await _dbContext.Appointments.FindAsync(appointmentId);
            if (traveler == null)
                return NotFound($"Traveler with Id = {id} not found");

            if (appointment == null)
                return NotFound($"Appointment with Id = {id} not found");

            var travelerAppointment = new TravelerAppointment
            {
                UserId = traveler.Id,
                AppointmentId = appointmentId
            };
            _dbContext.TravelerAppointments.Add(travelerAppointment);

            var request = new AppointmentRequest
            {
                Status = "Pending",
                TravelerAppointmentId = travelerAppointment.Id
            };
            _dbContext.AppointmentRequests.Add(request);

            await _dbContext.SaveChangesAsync();

            return Ok("Request added successfully");
        }
    }
}