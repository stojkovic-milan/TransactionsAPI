using TransactionsAPI.Application.TodoLists.Queries.ExportTodos;

namespace TransactionsAPI.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
