using Creations.Application.TodoLists.Queries.ExportTodos;

namespace Creations.Application.Common.Interfaces;
public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
