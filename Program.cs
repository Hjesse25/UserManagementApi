var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var users = new List<User>
{
    new User { Id = 1, Name = "Alex", Email = "alex@example.com" },
    new User { Id = 2, Name = "Maria", Email = "maria@example.com" }
};

app.MapGet("/", () => "I am Root!");

// Retrieves all users
app.MapGet("/users", () =>
{
    return users;
});

// Retrieves user by id
app.MapGet("/users/{id}", (int id) =>
{
    if (id < 0 || id >= users.Count)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(users[id]);
    }
});

// Create new user
app.MapPost("/users", (User newUser) =>
{
    newUser.Id = users.Count + 1;
    users.Add(newUser);
    return Results.Created($"/users/{newUser.Id}", newUser);
});

// Update user by Id
app.MapPut("/users/{id}", (int id, User updatedUser) =>
{
    if (id < 0 || id >= users.Count)
    {
        return Results.NotFound();
    }
    else
    {
        users[id] = updatedUser;
        return Results.Ok(updatedUser);
    }
});

app.MapDelete("/users/{id}", (int id) =>
{
    if (id < 0 || id >= users.Count)
    {
        return Results.NotFound();
    }
    else
    {
        users.RemoveAt(id);
        return Results.NoContent();
    }
});

app.Run();