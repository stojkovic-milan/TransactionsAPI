using TransactionsAPI.Application.Common.Interfaces;
using TransactionsAPI.Domain.Entities;
using MediatR;

namespace TransactionsAPI.Application.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand : IRequest<Guid>
{
    public string? ExternalTransactionId { get; init; }
    public User User { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string? TransactionHash { get; set; }
}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {

        var entity = new Transaction();

        entity.ExternalTransactionId = request.ExternalTransactionId;
        entity.User = request.User;
        entity.Amount = request.Amount;
        entity.Currency = request.Currency;
        entity.Id = Guid.NewGuid();

        _context.Transactions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}