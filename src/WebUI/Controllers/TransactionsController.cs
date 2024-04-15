using TransactionsAPI.Application.Transactions.Commands.CreateTransaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionsAPI.Application.Common.Interfaces;

namespace TransactionsAPI.WebUI.Controllers;

public class TransactionsController : ApiControllerBase
{
    private readonly IHeaderPropertyAccessService _headerPropertyAccessService;


    public TransactionsController(IHeaderPropertyAccessService headerPropertyAccessService)
    {
        _headerPropertyAccessService = headerPropertyAccessService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateTransactionCommand command)
    {
        command.TransactionHash = _headerPropertyAccessService.GetPropertyValue("hash");
        return await Mediator.Send(command);
    }
}