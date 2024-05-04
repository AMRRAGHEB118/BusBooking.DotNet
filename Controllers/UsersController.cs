using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusBooking.DotNet.data;
using BusBooking.DotNet.Models;
using BusBooking.DotNet.Dto;
using Microsoft.AspNetCore.Identity;

namespace BusBooking.DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public UsersController(UserManager<AppUser> userManager) {
            _userManager = userManager;
        }
        private readonly UserManager<AppUser> _userManager;
        [HttpPost]
        public async Task<ActionResult> AddUser(DtoNewUser user) {
            if(ModelState.IsValid) {
                AppUser newUser = new()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                if(result.Succeeded) {
                    return Ok("User created");
                }
                else {
                    foreach(IdentityError error in result.Errors) {
                        ModelState.AddModelError("", error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }
    }
}