using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.Entities
{
    public class TransactionContext :DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
