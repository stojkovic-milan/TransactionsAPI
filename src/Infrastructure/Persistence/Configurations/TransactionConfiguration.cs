using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionsAPI.Domain.Entities;
using TransactionsAPI.Infrastructure.Identity;

namespace TransactionsAPI.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasOne<ApplicationUser>().WithMany(u => u.Transactions).HasForeignKey(t => t.UserId);
    }
}
