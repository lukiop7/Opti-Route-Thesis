using OptiRoute.Application.Common.Mappings;
using OptiRoute.Domain.Entities;

namespace OptiRoute.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}