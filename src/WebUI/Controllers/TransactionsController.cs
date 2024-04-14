using TransactionsAPI.Application.Transactions.Commands.CreateTransaction;
using TransactionsAPI.Application.TodoLists.Commands.DeleteTodoList;
using TransactionsAPI.Application.TodoLists.Commands.UpdateTodoList;
using TransactionsAPI.Application.TodoLists.Queries.ExportTodos;
using TransactionsAPI.Application.TodoLists.Queries.GetTodos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TransactionsAPI.WebUI.Controllers;

[Authorize]
public class TransactionsController : ApiControllerBase
{

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTransactionCommand command)
    {
        return await Mediator.Send(command);
    }
}
