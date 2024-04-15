using Microsoft.AspNetCore.Identity;
using TransactionsAPI.Domain.Entities;

namespace TransactionsAPI.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public decimal Balance { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}
