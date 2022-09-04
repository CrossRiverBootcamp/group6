using AutoMapper;
using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;

namespace CustomerAccount.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private IAccountDal _accountDal;
        private IMapper _mapper;

        public CustomerService(IAccountDal accountDal)
        {
            _accountDal = accountDal;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperModelEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public async Task<bool> Register(RegisterModel accountModel)
        {
            bool duplicated = await _accountDal.EmailExists(accountModel.Email);
            //email exsits
            if (duplicated)
                return false;
            accountModel.Balance = 1000;
            accountModel.OpenDate = DateTime.UtcNow;
            Account account = _mapper.Map<Account>(accountModel);
            Customer customer = _mapper.Map<Customer>(accountModel);
            account.Customer = customer;
            customer.Salt = "rteyyww";
            //add salt to account!
            bool success = await _accountDal.CreateAccount(account,customer);
            return success;
        }

 
    }
}
