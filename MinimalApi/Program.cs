using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using MinimalApi.Data;
using MinimalApi.Modeles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ProductsDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//add post
app.MapPost("/products", async (Product product, AppDbContext db) =>
{
    await db.products.AddAsync(product);
    await db.SaveChangesAsync();
    return Results.Created($"{product.Id}", product);
});

//add get all
app.MapGet("/products", async (AppDbContext db) => await db.products.ToListAsync());


//add get by id
app.MapGet("/products/{id}", async (int id, AppDbContext db) =>
    await db.products.FindAsync(id)
    is Product product ? Results.Ok(product) : Results.NotFound());

app.MapPut("/products/{id}", async (int id ,Product input, AppDbContext db) =>
{
    var product = await db.products.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Name = input.Name;
    product.Price = input.Price;
    await db.SaveChangesAsync();
    return Results.Created($"{product.Id}", product);
});
app.Run();