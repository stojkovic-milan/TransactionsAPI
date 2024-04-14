namespace TransactionsAPI.Domain.Entities;

public class User : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public List<Transaction> TransactionList { get; set; } = new List<Transaction>();
}