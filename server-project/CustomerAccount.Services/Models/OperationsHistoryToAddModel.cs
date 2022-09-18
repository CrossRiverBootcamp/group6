namespace CustomerAccount.Services.Models;

public class OperationsHistoryToAddModel
{
    public int TransactionID { get; set; }
    public int FromAccountID { get; set; }
    public int ToAccountID { get; set; }
    public int Amount { get; set; }
}

