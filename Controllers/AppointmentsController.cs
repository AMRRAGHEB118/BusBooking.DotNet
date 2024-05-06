using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusBooking.DotNet.data;
using BusBooking.DotNet.Models;
using BusBooking.DotNet.Dto;

namespace BusBooking.DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController(DataContext dbContext) : ControllerBase
    {
        public class RetrievedAppointment {
            public int Id { get; set; }
            public int BusDestinationId { get; set; }
            public DateTime DateTime { get; set; }
            public int Capacity { get; set; }
            public int Booked { get; set; }
            public required string DestinationName { get; set; }
        } 
        private readonly DataContext _dbContext = dbContext;

        [HttpGet]
        public async Task<ActionResult<List<RetrievedAppointment>>> Get()
        {
            var appointments = await (from appointment in _dbContext.Appointments
                                    join busDestination in _dbContext.BusDestinations on appointment.BusDestinationId equals busDestination.Id
                                    select new RetrievedAppointment {
                                        Id = appointment.Id,
                                        BusDestinationId = appointment.BusDestinationId,
                                        DateTime = appointment.DateTime,
                                        Capacity = appointment.Capacity,
                                        Booked = appointment.Booked,
                                        DestinationName = busDestination.DestinationName
                                    }
                                ).ToListAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> Get(int id)
        {
            var appointment = await _dbContext.Appointments.FindAsync(id);
            if (appointment == null)
                return BadRequest("Appointment not found");
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<List<Appointment>>> AddAppointment(DtoNewAppointment newAppointment)
        {
            var dbBusDestination = await _dbContext.BusDestinations.FindAsync(newAppointment.BusDestinationId);

            if (dbBusDestination == null)
                return BadRequest("Bus Destination not found");

            _  = _dbContext.Appointments.Add(new Appointment
            {
                BusDestinationId = newAppointment.BusDestinationId,
                DateTime = newAppointment.DateTime,
                Capacity = newAppointment.Capacity
            });

            _ = await _dbContext.SaveChangesAsync();

            var appointments = await (from appointment in _dbContext.Appointments
                                    join busDestination in _dbContext.BusDestinations on appointment.BusDestinationId equals busDestination.Id
                                    select new RetrievedAppointment {
                                        Id = appointment.Id,
                                        BusDestinationId = appointment.BusDestinationId,
                                        DateTime = appointment.DateTime,
                                        Capacity = appointment.Capacity,
                                        Booked = appointment.Booked,
                                        DestinationName = busDestination.DestinationName
                                    }
                                ).ToListAsync();
            return Ok(appointments);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<RetrievedAppointment>>> UpdateAppointment(DateTime dateTime, int id)
        {
            var dbAppointment = await _dbContext.Appointments.FindAsync(id);
            if (dbAppointment == null)
                return NotFound($"Appointment with Id = {id} not found");

            dbAppointment.DateTime = dateTime;
            _ = await _dbContext.SaveChangesAsync();
            

            var appointments = await (from appointment in _dbContext.Appointments
                                    join busDestination in _dbContext.BusDestinations on appointment.BusDestinationId equals busDestination.Id
                                    select new RetrievedAppointment {
                                        Id = appointment.Id,
                                        BusDestinationId = appointment.BusDestinationId,
                                        DateTime = appointment.DateTime,
                                        Capacity = appointment.Capacity,
                                        Booked = appointment.Booked,
                                        DestinationName = busDestination.DestinationName
                                    }
                                ).ToListAsync();
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RetrievedAppointment>>> Delete(int id)
        {
            var dbAppointment = await _dbContext.Appointments.FindAsync(id);
            if (dbAppointment == null)
                return NotFound($"Appointment with Id = {id} not found");

            _ = _dbContext.Appointments.Remove(dbAppointment);
            _ = await _dbContext.SaveChangesAsync();

            var appointments = await (from appointment in _dbContext.Appointments
                                    join busDestination in _dbContext.BusDestinations on appointment.BusDestinationId equals busDestination.Id
                                    select new RetrievedAppointment {
                                        Id = appointment.Id,
                                        BusDestinationId = appointment.BusDestinationId,
                                        DateTime = appointment.DateTime,
                                        Capacity = appointment.Capacity,
                                        Booked = appointment.Booked,
                                        DestinationName = busDestination.DestinationName
                                    }
                                ).ToListAsync();
            return Ok(appointments);
        }  
    }
}