using System.Security.Cryptography;
using TransactionsAPI.Application.Common.Interfaces;
using TransactionsAPI.Domain.Entities;
using MediatR;
using System.Text;
using AutoMapper.QueryableExtensions;
using TransactionsAPI.Application.Common.Exceptions;

namespace TransactionsAPI.Application.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand : IRequest<Guid>
{
    public string ExternalTransactionId { get; init; } = String.Empty;
    public string UserId { get; set; } = String.Empty;
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
        //TODO: Move to appsettings.json and add service for fetching it
        string secretKey = "jKhluHU3PG9BTc7mtqN2R0K3xlW4bXiw";

        string hashValue = "";
        using (var sha256 = SHA256.Create())
        {
            var requestBodyString = "";
            requestBodyString += request.ExternalTransactionId;
            requestBodyString += request.UserId;
            requestBodyString += request.Currency;
            requestBodyString += request.Amount;

            byte[] bytes = Encoding.UTF8.GetBytes(requestBodyString + secretKey);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            hashValue = Convert.ToBase64String(hashBytes);
        }

        if (hashValue != request.TransactionHash)
            throw new InvalidHashException();

        entity.ExternalTransactionId = request.ExternalTransactionId;
        entity.User = _context.Users.FirstOrDefault(u => u.Id.ToString() == request.UserId);
        entity.Amount = request.Amount;
        entity.Currency = request.Currency;
        entity.Id = Guid.NewGuid();

        _context.Transactions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}