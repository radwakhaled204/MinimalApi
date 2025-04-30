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


app.MapGet("/products", async (AppDbContext db) => await db.products.ToListAsync());



app.MapGet("/products/{id}", async (int id, AppDbContext db) =>

    await db.products.FindAsync(id)
    is Product product ? Results.Ok(product) : Results.NotFound());

app.Run();