namespace CustomerAccount.Services.Models;

public class UpdateBalanceModel
{
    public int FromAccountID { get; set; }
    public int ToAccountID { get; set; }
    public int Amount { get; set; }
}

