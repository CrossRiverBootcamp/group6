using CustomerAccount.Data;
using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerAccount.Services.Extensions
{
    public static class ServicesContainerExtension
    {
        public static void AddServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IAccountDal, AccountDal>();
            services.AddDbContextFactory<CustomerAccountContext>(opt => opt.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=CustomerAccount;Integrated Security=True"));
        }
    }
}
