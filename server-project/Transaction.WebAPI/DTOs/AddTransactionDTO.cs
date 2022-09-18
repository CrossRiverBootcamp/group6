using System.ComponentModel.DataAnnotations;

namespace Transaction.WebAPI.DTOs;

public class AddTransactionDTO
{
    [Required]
    public int FromAccountId { get; set; }
    [Required]
    public int ToAccountID { get; set; }
    [Range(1, 1000000)]
    [Required]
    public int Amount { get; set; }
}
