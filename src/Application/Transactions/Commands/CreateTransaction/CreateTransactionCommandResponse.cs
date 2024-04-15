using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsAPI.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandResponse
{
    public string TransactionId { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public int Status { get; set; }
}