using Microsoft.EntityFrameworkCore;


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
