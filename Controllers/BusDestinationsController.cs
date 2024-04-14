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
    public class BusDestinationsController : ControllerBase
    {
        BusDestinationsController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly DataContext _dbContext;

        [HttpGet]
        public async Task<ActionResult<List<BusDestination>>> Get()
        {
            var destinations = await _dbContext.BusDestinations.ToListAsync();
            return Ok(destinations);
        }

        [HttpPost]
        public async Task<ActionResult<List<BusDestination>>> AddDestination(BusDestination destination)
        {
            _dbContext.BusDestinations.Add(destination);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.BusDestinations.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<BusDestination>>> UpdateDestination(String destination , int id)
        {
            var dbDestination = await _dbContext.BusDestinations.FindAsync(id);
            if (dbDestination == null)
                return NotFound($"Destination with Id = {id} not found");

            dbDestination.DestinationName = destination;
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.BusDestinations.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<BusDestination>>> Delete(int id)
        {
            var dbDestination = await _dbContext.BusDestinations.FindAsync(id);
            if (dbDestination == null)
                return NotFound($"Destination with Id = {id} not found");

            _dbContext.BusDestinations.Remove(dbDestination);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.BusDestinations.ToListAsync());
        }
    }
}