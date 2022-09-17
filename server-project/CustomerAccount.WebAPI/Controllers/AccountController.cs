using AutoMapper;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using CustomerAccount.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebAPI.Controllers
{

   
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperDtoModels>();
            });
            _mapper = config.CreateMapper();
        }
        [Authorize(Roles = "customer")]
        [HttpGet("{accountID}")]
        public async Task<ActionResult<AccountInfoDTO>> GetAccountInfo(int accountID)
        {
            if (accountID <= 0)
            {
                return BadRequest();
            }
            AccountModel account = await _accountService.GetAccountInfo(accountID);

            AccountInfoDTO accountInfo = _mapper.Map<AccountInfoDTO>(account);
            return Ok(accountInfo);
        }
        [Authorize(Roles = "customer")]
        [HttpGet()]
        public async Task<ActionResult<ForeignAccountDetailsDTO>> GetForeignAccountDetails(int foreignAccountID)
        {
            if (foreignAccountID <= 0)
            {
                return BadRequest();
            }
            AccountModel account = await _accountService.GetAccountInfo(foreignAccountID);

            ForeignAccountDetailsDTO accountInfo = _mapper.Map<ForeignAccountDetailsDTO>(account);
            return Ok(accountInfo);
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                LoginResultModel loginResult = await _accountService.Login(loginDTO.Email, loginDTO.Password);

                if (loginResult.AccountID <= 0)
                {
                    return Unauthorized();
                }

                LoginResultDTO loginResultDTO = _mapper.Map<LoginResultDTO>(loginResult);
                return Ok(loginResultDTO);
            }
            catch
            {
                return Unauthorized();
            }
        }

    }
}
