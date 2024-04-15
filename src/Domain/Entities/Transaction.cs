namespace TransactionsAPI.Domain.Entities;

public class Transaction : BaseEntity
{
    public string Id { get; set; }
    public string ExternalTransactionId { get; set; } = string.Empty;
    public string UserId { get; set; } = null!;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
}