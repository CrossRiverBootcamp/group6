using CustomerAccount.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebAPI.Controllers;

[Authorize(Roles = "customer")]
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
       bool success = await _emailVerificationService.AddEmailVerification(email);
        if (!success)
        {
            return BadRequest();
        }
        return Ok();
    }
}

