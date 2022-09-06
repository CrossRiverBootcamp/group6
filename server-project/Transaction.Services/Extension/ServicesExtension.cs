using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Data;
using Transaction.Data.Entities;
using Transaction.Data.Interfaces;

namespace Transaction.Services.Extension
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services ,string connection)
        {
            services.AddDbContextFactory<TransactionContext>(opt => opt.UseSqlServer(connection));
            services.AddScoped<ITransactionDal, TransactionDal>();
        }
    }
}
