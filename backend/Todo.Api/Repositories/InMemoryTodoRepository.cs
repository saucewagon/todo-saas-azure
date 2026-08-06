using Todo.Api.Models;

namespace Todo.Api.Repositories;

public class InMemoryTodoRepository : ITodoRepository
{
    private readonly List<TodoItem> _items = new();

    public IEnumerable<TodoItem> GetAll()
    {
        return _items;
    }

    public TodoItem Add(TodoItem item)
    {
        item.Id = Guid.NewGuid();

        _items.Add(item);

        return item;
    }
}