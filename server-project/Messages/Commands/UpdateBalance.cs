namespace Messages.Commands;

public class UpdateBalance
{
    public int TransactionID { get; set; }
    public Guid TransactionEventID { get; set; }
    public int FromAccountID { get; set; }
    public int ToAccountID { get; set; }
    public int Amount { get; set; }
}

