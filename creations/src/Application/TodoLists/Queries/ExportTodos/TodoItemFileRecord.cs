using Creations.Application.Common.Mappings;
using Creations.Domain.Entities;

namespace Creations.Application.TodoLists.Queries.ExportTodos;
public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
