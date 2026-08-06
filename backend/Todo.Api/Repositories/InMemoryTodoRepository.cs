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
    public TodoItem? Get(Guid id)
{
    return _items.FirstOrDefault(x => x.Id == id);
}

public bool Update(TodoItem item)
{
    var existing = Get(item.Id);

    if (existing is null)
        return false;

    existing.Title = item.Title;
    existing.Completed = item.Completed;

    return true;
}

public bool Delete(Guid id)
{
    var item = Get(id);

    if (item is null)
        return false;

    _items.Remove(item);

    return true;
}
}