using CustomerAccount.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustomerAccount.Data.Entities
{
    public class CustomerAccountContext : DbContext
    {

        public CustomerAccountContext(DbContextOptions<CustomerAccountContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<OperationsHistory> OperationsHistorys { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }

    }
}
