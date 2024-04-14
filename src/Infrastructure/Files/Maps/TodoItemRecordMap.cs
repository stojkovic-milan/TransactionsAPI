using System.Globalization;
using TransactionsAPI.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace TransactionsAPI.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
    }
}
