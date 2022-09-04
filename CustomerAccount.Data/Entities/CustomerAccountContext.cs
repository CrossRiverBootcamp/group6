using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.Data.Entities
{
    public class CustomerAccountContext:DbContext
    {
        public CustomerAccountContext(DbContextOptions<CustomerAccountContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
     
    }
}
