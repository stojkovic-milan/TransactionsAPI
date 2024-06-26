﻿using TransactionsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TransactionsAPI.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    DbSet<Transaction> Transactions { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}