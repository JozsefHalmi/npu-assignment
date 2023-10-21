using Creations.Application.Common.Mappings;
using Creations.Domain.Entities;

namespace Creations.Application.TodoLists.Queries.GetTodos;
public class TodoListDto : IMapFrom<TodoList>
{
    public TodoListDto()
    {
        Items = Array.Empty<TodoItemDto>();
    }

    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Colour { get; init; }

    public IReadOnlyCollection<TodoItemDto> Items { get; init; }
}
