using AutoMapper;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using CustomerAccount.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IEmailVerificationService _emailVerificationService;
    private readonly IMapper _mapper;

    public CustomerController(ICustomerService customerService,IEmailVerificationService emailVerificationService)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperDtoModels>();
        });
        _mapper = config.CreateMapper();
        _customerService = customerService;
        _emailVerificationService = emailVerificationService;
    }
    [HttpPost("CreateAccount")]
    public async Task<ActionResult<bool>> Register(RegisterDTO register)
    {
        bool verified = await _emailVerificationService.CheckVerification(register.Email,register.VerifiCode);
        if (!verified)
        {
            return BadRequest(false);
        }
        RegisterModel registerModel = _mapper.Map<RegisterModel>(register);
        bool success = await _customerService.Register(registerModel);
        return Ok(success);
    }
}
