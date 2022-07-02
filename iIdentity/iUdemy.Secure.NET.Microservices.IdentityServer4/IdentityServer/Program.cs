using IdentityServer;
using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddTestUsers(Config.TestUsers)
                .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();

app.MapGet("/", () => "IdentityServer Runing!");

app.Run();
