﻿using AutoMapper;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using CustomerAccount.WebAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
        private ICustomerService _customerService;
        private IMapper _mapper;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperDtoModels>();
            });
            _mapper = config.CreateMapper();
        }
        [HttpPost("CreateAccount")]
        public async Task<ActionResult<bool>> Register(RegisterDTO register)
        {
            RegisterModel registerModel = _mapper.Map<RegisterModel>(register);
            try
            {
                 bool success = await _customerService.Register(registerModel);
                return(success);
            } 
            catch
            {
                return Ok(false);

            }

                
        }
    }
}