using Todo.Api.Repositories;
using Todo.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/api/todos", (ITodoRepository repository) =>
{
    return repository.GetAll();
});

app.MapPost("/api/todos", (ITodoRepository repository, TodoItem item) =>
{
    return repository.Add(item);
});

app.Run();