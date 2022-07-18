using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityApp.Authorization;

namespace IdentityApp.Data;

public class SeedData
{
    public static async Task Initialize(
        IServiceProvider serviceProvider, string password)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            //Accountant(s)
            var userUid1 = await EnsureUser(serviceProvider, password, "user1@demo.com");
            var userUid2 = await EnsureUser(serviceProvider, password, "user2@demo.com");

            //Manager(s)
            var managerUid = await EnsureUser(serviceProvider, password, "manager@demo.com");
            await EnsureRole(serviceProvider, managerUid, Constants.InvoiceManagersRole);

            //Admin(s)
            var adminUid = await EnsureUser(serviceProvider, password, "admin@demo.com");
            await EnsureRole(serviceProvider, adminUid, Constants.InvoiceAdminRole);

        }

    }

    private static async Task<string> EnsureUser(
        IServiceProvider serviceProvider,
        string userName, string initPw)
    {
        var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

        var user = await userManager.FindByNameAsync(userName);

        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, initPw);

            if (!result.Succeeded)
                throw new Exception("User did not get created, Password Policy problem?");
        }

        return user.Id;
    }

    public static async Task<IdentityResult> EnsureRole(
        IServiceProvider serviceProvider,
        string uid, string role)
    {
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

        IdentityResult ir;

        if (await roleManager.RoleExistsAsync(role) == false)
        {
            ir = await roleManager.CreateAsync(new IdentityRole(role));
        }

        var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

        var user = await userManager.FindByIdAsync(uid);

        if (user == null)
            throw new Exception("User not existing");

        ir = await userManager.AddToRoleAsync(user, role);

        return ir;
    }
}
