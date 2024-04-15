﻿using System.Security.Cryptography;
using TransactionsAPI.Application.Common.Interfaces;
using TransactionsAPI.Domain.Entities;
using MediatR;
using System.Text;
using AutoMapper.QueryableExtensions;
using CleanTemplate.Domain.Events;
using TransactionsAPI.Application.Common.Exceptions;

namespace TransactionsAPI.Application.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand : IRequest<string>
{
    public string ExternalTransactionId { get; init; } = String.Empty;
    public string UserId { get; set; } = String.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string? TransactionHash { get; set; }
}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IHashKeyAccessService _hashKeyAccessService;

    public CreateTransactionCommandHandler(IApplicationDbContext context, IIdentityService identityService,
        IHashKeyAccessService hashKeyAccessService)
    {
        _context = context;
        _identityService = identityService;
        _hashKeyAccessService = hashKeyAccessService;
    }

    public async Task<string> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        
        var entity = new Transaction();
        string secretKey = _hashKeyAccessService.SecretKey;

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
        entity.UserId = request.UserId;
        entity.Amount = request.Amount;
        entity.Currency = request.Currency;
        entity.Id = Guid.NewGuid().ToString();

        entity.AddDomainEvent(new TransactionCreatedEvent(entity));

        _context.Transactions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}