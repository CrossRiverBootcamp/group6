using AutoMapper;
using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Exceptions;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.Services.Services
{
    public class AccountService : IAccountService
    {
        private IAccountDal _accountDal;
        private IMapper _mapper;
        private IPasswordHashService _passwordHashService;

        public AccountService(IAccountDal accountDal, IPasswordHashService passwordHashService)
        {
            _accountDal = accountDal;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperModelEntity>();
            });
            _mapper = config.CreateMapper();
            _passwordHashService = passwordHashService;
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
        public async Task<AccountModel> GetAccountInfo(int AccountID)
        {
            try
            {
                Account account = await _accountDal.GetAccountInfo(AccountID);
                return _mapper.Map<AccountModel>(account);
            }
            catch
            {
                throw new KeyNotFoundException("fail to get accountInfo by accountID");
            }
             
           
           
        }

        public async Task<int> Login(string email, string Password)
        {
            bool exists = await _accountDal.EmailExists(email);
            if (!exists)
            {
                throw new EmailNotFoundException("there is no such an email");
            }
            try
            {
                    Customer customer = await _accountDal.GetCustomerByEmail(email);
                    string hashPassword = _passwordHashService.HashPassword(Password, customer.Salt, 1000, 8);
                if (hashPassword.Equals(customer.Password.TrimEnd()))
                      return await _accountDal.Login(email, hashPassword);
                else
                {
                        throw new NotValidException("password is not right!!!-");
                } 
            }
            catch
            {
                throw new NoAccessException("login not successed");
            }
   
           
        }
        public async Task<string> UpdateAccounts(UpdateBalance updateBalanceModel)
        { 
            //not null obj
            if (updateBalanceModel == null) { return "missing deatels"; }
            //check correctness of accounts ids
            Account accountFrom = await _accountDal.FindUpdateAccount(updateBalanceModel.FromAccountID);
            Account accountTo= await _accountDal.FindUpdateAccount(updateBalanceModel.ToAccountID);
            if (accountFrom == null || accountTo == null) { return "not the right number account"; }
            // check sender balance
            if (accountFrom.Balance < updateBalanceModel.Amount) { return "not inof mony in the account"; }
           //update balance
            accountFrom.Balance-=updateBalanceModel.Amount;
            accountTo.Balance+=updateBalanceModel.Amount;
            return await _accountDal.UpdateAccounts(accountFrom, accountTo);
            
        }
    }
}
 