using CustomerAccount.Services.Models;


namespace CustomerAccount.Services.Interfaces
{
    public interface ICustomerService
    {
        bool Register(RegisterModel accountModel);
        // int Login(string UserName, string Password);
    }
}
