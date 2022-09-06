using AutoMapper;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using CustomerAccount.WebAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IMapper _mapper;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperDtoModels>();
            });
            _mapper = config.CreateMapper();
        }
        [HttpGet("{accountID}")]
        public async Task<ActionResult<AccountInfoDTO>> GetAccountInfo(int accountID)
        {
            if (accountID<10)
            {
                return BadRequest();
            }
           AccountModel account=await _accountService.GetAccountInfo(accountID);
            if (account is null)
            {
                return NotFound();
            }
           AccountInfoDTO accountInfo = _mapper.Map<AccountInfoDTO>(account);
            return Ok(accountInfo);
        }
        [HttpPost("login")]
        public async Task<ActionResult<int>> Login(LoginDTO person)
        {
            if (person is null)
            {
                return BadRequest();
            }
            int accountID =await _accountService.Login(person.Email, person.Password);
            if (accountID<10)
            {
                return Unauthorized();
            }
            return Ok(accountID);
        }

    }
}
