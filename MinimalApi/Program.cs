using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

using MinimalApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ProductsDb"));



var app = builder.Build();
app.Run();