using TransactionsAPI.Application.Common.Mappings;
using TransactionsAPI.Domain.Entities;

namespace TransactionsAPI.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
