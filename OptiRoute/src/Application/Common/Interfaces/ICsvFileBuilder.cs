using OptiRoute.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace OptiRoute.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}