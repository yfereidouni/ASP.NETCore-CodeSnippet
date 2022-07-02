using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoviesAPIContext>(options => options
                .UseInMemoryDatabase("Movies"));

builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options => 
                {
                    options.Authority = "https://localhost:5005"; //IdentityServer address
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

builder.Services.AddAuthorization(options => 
{
    options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "movieClient"));
});

var app = builder.Build();

///SeedData-To-InMemory-Database --------------------------------------
#region Way-1-of-Seed-Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MoviesAPIContext>();
    SeedData.Initialize(context);
}
#endregion

#region Way-2-of-Seed-Data
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    SeedData.Initialize(services);
//}
#endregion
///---------------------------------------------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
