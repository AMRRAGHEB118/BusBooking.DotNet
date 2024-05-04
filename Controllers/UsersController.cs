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
    public class UsersController : ControllerBase
    {
        public UsersController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly DataContext _dbContext;


        // [HttpGet]
        // public async Task<ActionResult<List<AppUser>>> Get()
        // {
        //     var users = await _dbContext.Users.ToListAsync();
        //     return Ok(users);
        // }


        // [HttpPost]
        // public async Task<ActionResult<List<User>>> AddUser(User user)
        // {
        //     _dbContext.Users.Add(user);
        //     await _dbContext.SaveChangesAsync();
        //     return Ok(await _dbContext.Users.ToListAsync());
        // }


        // [HttpPut("{id}")]
        // public async Task<ActionResult<List<User>>> UpdateUser(User request, int id)
        // {
        //     var dbUser = await _dbContext.Users.FindAsync(id);
        //     if (dbUser == null)
        //         return NotFound($"User with Id = {id} not found");

        //     dbUser.UserName = request.Name;
        //     dbUser.Email = request.Email;
        //     dbUser. = request.Password;
        //     await _dbContext.SaveChangesAsync();
        //     return Ok(await _dbContext.Users.ToListAsync());
        // }


        // [HttpDelete("{id}")]
        // public async Task<ActionResult<List<AppUser>>> Delete(int id)
        // {
        //     var dbUser = await _dbContext.Users.FindAsync(id);
        //     if (dbUser == null)
        //         return NotFound($"User with Id = {id} not found");

        //     _dbContext.Users.Remove(dbUser);
        //     await _dbContext.SaveChangesAsync();
        //     return Ok(await _dbContext.Users.ToListAsync());
        // }
    }
}