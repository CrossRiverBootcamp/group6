using CustomerAccount.Services.Models;

namespace CustomerAccount.Services.Interfaces;
public interface ICustomerService
{
    Task<bool> Register(RegisterModel accountModel);

}
