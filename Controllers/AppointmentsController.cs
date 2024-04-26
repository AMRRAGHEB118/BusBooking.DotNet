using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusBooking.DotNet.data;
using BusBooking.DotNet.Models;

namespace BusBooking.DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        AppointmentsController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly DataContext _dbContext;

        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> Get()
        {
            var appointments = await _dbContext.Appointments.ToListAsync();
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
        public async Task<ActionResult<List<Appointment>>> AddAppointment(Appointment appointment)
        {
            var busDestination = await _dbContext.BusDestinations.FindAsync(appointment.BusDestinationId);

            if (busDestination == null)
                return BadRequest("Bus Destination not found");

            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Appointments.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Appointment>>> UpdateAppointment(Appointment request, int id)
        {
            var dbAppointment = await _dbContext.Appointments.FindAsync(id);
            if (dbAppointment == null)
                return NotFound($"Appointment with Id = {id} not found");

            dbAppointment.BusDestinationId = request.BusDestinationId;
            dbAppointment.DateTime = request.DateTime;
            dbAppointment.Capacity = request.Capacity;
            dbAppointment.Booked = request.Booked;
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Appointments.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Appointment>>> Delete(int id)
        {
            var dbAppointment = await _dbContext.Appointments.FindAsync(id);
            if (dbAppointment == null)
                return NotFound($"Appointment with Id = {id} not found");

            _dbContext.Appointments.Remove(dbAppointment);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Appointments.ToListAsync());
        }  
    }
}