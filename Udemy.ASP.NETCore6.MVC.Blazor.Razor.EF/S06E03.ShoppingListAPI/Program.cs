using Microsoft.EntityFrameworkCore;
using S06E03.ShoppingListAPI.Models;

/// -----------------------------------------
/// "ASPNETCORE_ENVIRONMENT": "Development"
/// "ASPNETCORE_ENVIRONMENT": "Staging"
/// https://localhost:5000/api/errors/throw
/// -----------------------------------------

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShoppingListContext>(options =>
    options.UseInMemoryDatabase("Groceries_DB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    //app.UseExceptionHandler("/error-development");
}
else
{
    //app.UseExceptionHandler("/error");
}

// SeedData: -----------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}
//-----------------------------------------------------------------------

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
