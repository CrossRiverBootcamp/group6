namespace Transaction.Services.Models;

public class StausModel
{
    public int TransactionID { get; set; }
    public bool Success { get; set; }
    public string? FailureReason { get; set; }

}

