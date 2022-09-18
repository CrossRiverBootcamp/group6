using CustomerAccount.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebAPI.Controllers;

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
    public async Task<ActionResult> Post([FromBody] string email)
    {
        String emailAdress=new String(email);
        bool success = await _emailVerificationService.AddEmailVerification(emailAdress);
        if (!success)
        {
            return BadRequest();
        }
        return Ok();
    }
}

