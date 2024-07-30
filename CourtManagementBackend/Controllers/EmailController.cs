using Microsoft.AspNetCore.Mvc;
using CourtManagementBackend.Models;
using CourtManagementBackend.Services;
using System.Threading.Tasks;

namespace CourtManagementBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-notification")]
        public async Task<IActionResult> SendEmailNotification([FromBody] Case newCase)
        {
            try
            {
                await _emailService.SendEmailNotificationAsync(newCase);
                return Ok("Email notification sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
    }

}
