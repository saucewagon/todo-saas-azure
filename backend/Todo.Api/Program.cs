using Todo.Api.Dtos;
using Todo.Api.Filters;
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

app.MapPost("/api/todos", (ITodoRepository repository, CreateTodoRequest request, ILogger<Program> logger) =>
{
    logger.LogInformation("Creating todo with title: {Title}", request.Title);
    var todo = new TodoItem
    {
        Title = request.Title,
        Completed = false
    };
    logger.LogInformation("Created todo with id: {Id}", todo.Id);

    return repository.Add(todo);
})
.AddEndpointFilter(new ValidationFilter<CreateTodoRequest>());

app.MapPut("/api/todos/{id:guid}", (Guid id, ITodoRepository repository, UpdateTodoRequest request) =>
{
    var todo = new TodoItem
    {
        Id = id,
        Title = request.Title,
        Completed = request.Completed
    };

    if (!repository.Update(todo))
    {
        return Results.NotFound();
    }

    return Results.NoContent();
})
.AddEndpointFilter(new ValidationFilter<CreateTodoRequest>());

app.MapDelete("/api/todos/{id:guid}", (Guid id, ITodoRepository repository) =>
{
    if (!repository.Delete(id))
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});
app.Run();