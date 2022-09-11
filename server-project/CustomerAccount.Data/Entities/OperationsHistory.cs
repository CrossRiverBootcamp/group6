using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace CustomerAccount.Data.Entities;

public class OperationsHistory
{
    [Key]
    public Guid ID { get; set; }
    [Required]
    public int AccountId { get; set; }
    [Required]
    public int TransactionID { get; set; }
    [Required]
    public bool Credit { get; set; }
    [Required]
    public int TransactionAmount { get; set; }
    [Required]
    public int Balance { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime OperationTime { get; set; }
    [JsonIgnore]
    [ForeignKey("AccountId")]
    public virtual Account Account { get; set; }

}

