using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myapp.Data;
using myapp.Models;

namespace myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TestApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNotification([FromBody] Notification dto)
        {
            try
            {
                var notification = new Notification
                {
                    Username = dto.Username,
                    Message = dto.Message,
                    MessageType = dto.MessageType,
                };
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();
                return Ok(new {
                    Message="Successfully created notification"
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}