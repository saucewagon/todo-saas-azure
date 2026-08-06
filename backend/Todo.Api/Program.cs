using Todo.Api.Repositories;
using Todo.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();

var app = builder.Build();
app.UseCors("frontend");

app.UseHttpsRedirection();

app.MapGet("/api/todos", (ITodoRepository repository) =>
{
    return repository.GetAll();
});

app.MapPost("/api/todos", (ITodoRepository repository, TodoItem item) =>
{
    return repository.Add(item);
});
app.MapPut("/api/todos/{id:guid}", (Guid id, ITodoRepository repository, TodoItem item) =>
{
    item.Id = id;

    if (!repository.Update(item))
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});
app.MapDelete("/api/todos/{id:guid}", (Guid id, ITodoRepository repository) =>
{
    if (!repository.Delete(id))
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});
app.Run();