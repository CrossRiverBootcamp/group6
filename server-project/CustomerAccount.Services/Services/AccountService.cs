using AutoMapper;
using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using Messages.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomerAccount.Services.Services;

public class AccountService : IAccountService
{
    private IAccountDal _accountDal;
    private IMapper _mapper;
    private IPasswordHashService _passwordHashService;
    private IConfiguration _configuration;

    public AccountService(IAccountDal accountDal, IPasswordHashService passwordHashService, IConfiguration configuration)
    {
        _accountDal = accountDal;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperModelEntity>();
        });
        _mapper = config.CreateMapper();
        _passwordHashService = passwordHashService;
        _configuration = configuration;
    }
    public AccountService(IAccountDal accountDal)
    {
        _accountDal = accountDal;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperModelEntity>();
        });
        _mapper = config.CreateMapper();
    }
    public async Task<AccountModel> GetAccountInfo(int accountID)
    {
        Account account = await _accountDal.GetAccountInfo(accountID);
        return _mapper.Map<AccountModel>(account);
    }
    public async Task<LoginResultModel?> Login(string email, string password)
    {
        string? salt = await _accountDal.GetSaltByEmail(email);
        if (salt is null)
        {
            return null;
        }
        string hashPassword = _passwordHashService.HashPassword(password, salt, 1000, 8);
        int? accountID = await _accountDal.Login(email, hashPassword);
        if (accountID is null)
        {
            return null;
        }
        string token = await GetToken(accountID.Value);
        LoginResultModel loginResult = new LoginResultModel()
        {
            AccountID = accountID.Value,
            Token = token,
        };
        return loginResult;
    }
    public async Task<string?> UpdateAccounts(UpdateBalance updateBalanceModel)
    {
        //check correctness of accounts ids
        Account? accountFrom = await _accountDal.FindUpdateAccount(updateBalanceModel.FromAccountID);
        Account? accountTo = await _accountDal.FindUpdateAccount(updateBalanceModel.ToAccountID);
        if (accountFrom is null || accountTo is null) {
            return "not the right number account";
        }

        // check sender balance
        if (accountFrom.Balance < updateBalanceModel.Amount) 
        { 
            return "not enogh money in the account"; 
        }

        //update balance
        accountFrom.Balance -= updateBalanceModel.Amount;
        accountTo.Balance += updateBalanceModel.Amount;
        await _accountDal.UpdateAccounts(accountFrom, accountTo);
        return null;
    }
    public async Task<string> GetToken(int accountID)
    {
        //create claims details based on the user information
        var claims = new[] {
                        new Claim("AccountID",accountID.ToString()),
                        new Claim(ClaimTypes.Role, "customer")
                    };
        var issuer = "https://exemple.com";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer,
            issuer,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signIn);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

