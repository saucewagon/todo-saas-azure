using Todo.Api.Data;
using Todo.Api.Models;

namespace Todo.Api.Repositories;

public class EfTodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public EfTodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TodoItem> GetAll()
    {
        return _context.Todos.ToList();
    }

    public TodoItem Add(TodoItem item)
    {
        item.Id = Guid.NewGuid();

        _context.Todos.Add(item);
        _context.SaveChanges();

        return item;
    }

    public TodoItem? Get(Guid id)
    {
        return _context.Todos.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(TodoItem item)
    {
        var existing = Get(item.Id);

        if (existing is null)
            return false;

        existing.Title = item.Title;
        existing.Completed = item.Completed;

        _context.SaveChanges();

        return true;
    }

    public bool Delete(Guid id)
    {
        var item = Get(id);

        if (item is null)
            return false;

        _context.Todos.Remove(item);
        _context.SaveChanges();

        return true;
    }
}