
namespace CustomerAccount.Services.Models;

    public class OperationsHistoryModel
    {
    public bool Credit { get; set; }
    public int AccountId { get; set; }
    public int Amount { get; set; }
    public int Balance { get; set; }
    public DateTime Date { get; set; }
    }


