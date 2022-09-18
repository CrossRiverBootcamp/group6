using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transaction.Data.Entities;

public class Transaction
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionID { get; set; }
    [Required]
    public int FromAccountID { get; set; }
    [Required]
    public int ToAccountID { get; set; }
    [Required]
    [Range(1, 1000000)]
    public int Amount { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public Status Status { get; set; }
    public string? FailureReason { get; set; }

}
public enum Status
{
    Processing, Success, Fail,
}
