using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("ToDoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.IncludeFields = true;
});

var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

var app = builder.Build();

app.MapGet("/", () => Results.Json(new Todo
{
    Name = "Create todo tasks easily",
    IsComplete = false
}, options));

app.MapGet("/todoitems", async (TodoDbContext dbContext) => await dbContext.Todos
    .Select(x => new TodoDTO(x))
    .ToListAsync());

app.MapGet("/todoitems/{id}", async (TodoDbContext dbContext, int id) => await dbContext.Todos
    .FindAsync(id) is Todo todo 
        ? Results.Ok(new TodoDTO(todo)) 
        : Results.NotFound());

app.MapPost("/todoitems", async (TodoDbContext dbContext, Todo todo) =>
{
    dbContext.Todos.Add(todo);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", new TodoDTO(todo));
});

app.MapPut("/todoitems/{id}", async (TodoDbContext dbContext, int id, Todo todo) =>
{
    var foundTodo = await dbContext.Todos.FindAsync(id);
    if (foundTodo is null) return Results.NotFound();

    foundTodo.Name = todo.Name;
    foundTodo.IsComplete = todo.IsComplete;

    await dbContext.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (TodoDbContext dbContext, int id) =>
{
    if (await dbContext.Todos.FindAsync(id) is Todo todo)
    {
        dbContext.Todos.Remove(todo);
        await dbContext.SaveChangesAsync();
        return Results.Ok(new TodoDTO(todo));
    }

    return Results.NotFound();
});


app.Run();

public class Todo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public string? Secret { get; set; }
}

public class TodoDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    public TodoDTO() { }

    public TodoDTO(Todo todo) => (Id, Name, IsComplete) = (todo.Id, todo.Name, todo.IsComplete);
}

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    { }

    public DbSet<Todo> Todos => Set<Todo>();
}