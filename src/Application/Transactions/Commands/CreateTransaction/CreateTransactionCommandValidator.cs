using TransactionsAPI.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace TransactionsAPI.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTransactionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.ExternalTransactionId)
            .NotEmpty().WithMessage("ExternalTransactionId is required.")
            .MustAsync(BeUniqueTransactionId).WithMessage("The specified ExternalTransactionId already exists.");

        RuleFor(v => v.Amount)
            .GreaterThan(0).WithMessage("Amount must be positive.");

        RuleFor(v => v.Currency)
            .NotEmpty().WithMessage("Currency is required.");
    }

    public async Task<bool> BeUniqueTransactionId(string externalId, CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .AllAsync(l => l.ExternalTransactionId != externalId, cancellationToken);
    }
}