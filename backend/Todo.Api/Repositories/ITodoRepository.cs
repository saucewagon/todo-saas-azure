using Todo.Api.Models;

namespace Todo.Api.Repositories;

public interface ITodoRepository
{
    IEnumerable<TodoItem> GetAll();

    TodoItem Add(TodoItem item);
}