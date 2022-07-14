using Microsoft.EntityFrameworkCore;

namespace S06E03.ShoppingListAPI.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ShoppingListContext(serviceProvider
            .GetRequiredService<DbContextOptions<ShoppingListContext>>()))
        {
            if (context.Groceries.Any())
            {
                return;
            }
            context.Groceries.AddRange(
                new Grocery { Id = 1,Name="Tomato", Purchased = true },
                new Grocery { Id = 2,Name="Cabage", Purchased = true },
                new Grocery { Id = 3,Name="Orange", Purchased = true },
                new Grocery { Id = 4,Name="Carrot", Purchased = true }
                );
            context.SaveChanges();
        }
    }
}
