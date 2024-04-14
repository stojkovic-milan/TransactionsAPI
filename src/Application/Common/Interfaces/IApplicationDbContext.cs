﻿using TransactionsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TransactionsAPI.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Transaction> Transactions { get; }

    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}