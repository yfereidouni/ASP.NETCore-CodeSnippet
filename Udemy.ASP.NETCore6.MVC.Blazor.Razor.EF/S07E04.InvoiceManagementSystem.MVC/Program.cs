using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using S07E04.InvoiceManagementSystem.MVC.Authorization;
using S07E04.InvoiceManagementSystem.MVC.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = true;

}).AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>();

#region More-Configuration
// More Configuration
//builder.Services.Configure<IdentityOptions>(options => 
//{
//    options.SignIn.RequireConfirmedEmail = false;
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Lockout.MaxFailedAccessAttempts = 3;
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
//    options.Lockout.AllowedForNewUsers = true;
//    options.User.RequireUniqueEmail = true;
//});
#endregion

builder.Services.AddControllersWithViews();

//----------------------------------------------------------------
//Not showing any page and redirect them to LOGIN form.
//Unless you except the pages with the following attribute:
//[AllowAnonymous]
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
//----------------------------------------------------------------


builder.Services.AddScoped<IAuthorizationHandler, InvoiceCreatorAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, InvoiceManagerAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, InvoiceAdminAuthorizationHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    //Auto-Migrate Database
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    // Manage User Secrets
    var seedUserPass = builder.Configuration.GetValue<string>("SeedUserPass");
    await SeedData.Initialize(services,seedUserPass);
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
