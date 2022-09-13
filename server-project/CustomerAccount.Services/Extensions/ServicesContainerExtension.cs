using CustomerAccount.Data;
using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace CustomerAccount.Services.Extensions
{
    public static class ServicesContainerExtension
    {
        public static void AddServiceExtension(this IServiceCollection services,string connection)
        {
            services.AddScoped<IAccountDal, AccountDal>();
            services.AddScoped<IOperationsHistoryDal, OperationsHistoryDal>();
            services.AddScoped<IEmailVerificationDal, EmailVerificationDal>();
            services.AddDbContextFactory<CustomerAccountContext>(opt => opt.UseSqlServer(connection));

        }
    }
}

