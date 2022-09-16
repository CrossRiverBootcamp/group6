using AutoMapper;
using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using CustomExceptions;

namespace CustomerAccount.Services.Services;

    public class CustomerService : ICustomerService
    {
        private IAccountDal _accountDal;
        private IMapper _mapper;
        private IPasswordHashService _passwordHashService;

        public CustomerService(IAccountDal accountDal, IPasswordHashService passwordHashService)
        {
           
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperModelEntity>();
            });
            _mapper = config.CreateMapper();
            _accountDal = accountDal;
            _passwordHashService = passwordHashService;
        }
        public async Task<bool> Register(RegisterModel accountModel)
        {
            bool duplicated = await _accountDal.EmailExists(accountModel.Email);
            //email exsits
            if (duplicated)
                throw new DuplicatedException("email address in use!!");
          
            accountModel.Balance = 1000;
            accountModel.OpenDate = DateTime.UtcNow;
            Account account = _mapper.Map<Account>(accountModel);
            Customer customer = _mapper.Map<Customer>(accountModel);
            account.Customer = customer;
            try
            {
                customer.Salt = _passwordHashService.GenerateSalt(8);
                customer.Password = _passwordHashService.HashPassword(customer.Password, customer.Salt, 1000, 8);
                bool success = await _accountDal.CreateAccount(account,customer);
                return success;
            }
            catch
            {
                throw new NotSavedException("customer & account not Created");
            }
        }
    }

