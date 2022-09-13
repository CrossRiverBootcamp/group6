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

    private ICustomerService _customerService;
    private IMapper _mapper;

    public CustomerController(ICustomerService customerService)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperDtoModels>();
        });
        _mapper = config.CreateMapper();
        _customerService = customerService;
    }
    [HttpPost("CreateAccount")]
    public async Task<ActionResult<bool>> Register(RegisterDTO register)
    {
        RegisterModel registerModel = _mapper.Map<RegisterModel>(register);
        bool success = await _customerService.Register(registerModel);
        return Ok(success);
    }
}
