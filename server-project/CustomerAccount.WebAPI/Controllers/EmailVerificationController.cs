using CustomerAccount.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAccount.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailVerificationController : ControllerBase
    {
        private readonly IEmailVerificationService _emailVerificationService;
        public EmailVerificationController(IEmailVerificationService emailVerificationService)
        {
            _emailVerificationService = emailVerificationService;
        }
        
  

        // POST api/<EmailVerificationController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] string email)
        {
            try
            {
                await _emailVerificationService.AddEmailVerification(email);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }

    }
}
