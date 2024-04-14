namespace TransactionsAPI.Domain.Entities;

public class Transaction : BaseEntity
{
    public Guid Id { get; set; }
    public string ExternalTransactionId { get; set; } = string.Empty;
    public User User { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
}