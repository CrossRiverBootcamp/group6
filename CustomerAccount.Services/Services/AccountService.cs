using AutoMapper;
using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
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
            Account account =await _accountDal.GetAccountInfo(AccountID);
            //exceptions---
            if (account is null)
            {
                return null;
            }
            return _mapper.Map<AccountModel>(account);
           
        }

        public async Task<int> Login(string email, string Password)
        {
           bool exsits =await _accountDal.EmailExists(email);
            if (!exsits)
            {
                return -1;
            }
            return _accountDal.Login(email, Password);
        }
    }
}
