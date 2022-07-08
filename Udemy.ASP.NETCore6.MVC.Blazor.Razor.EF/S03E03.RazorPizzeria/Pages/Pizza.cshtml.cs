using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPizzeria.Models;

namespace RazorPizzeria.Pages
{
    public class PizzaModel : PageModel
    {
        public List<PizzasModel> FakePizzaDB = new List<PizzasModel>()
        {
            new PizzasModel()
            {
              ImageTitle = "Margherita",
              PizzaName = "Margherita",
              BasePrice = 2,
              TomatoSauce = true,
              Cheese = true,
              Beef = false,
              Ham = false,
              Mushroom = false,
              Pepperoni = false,
              Pineapple = false,
              Tuna = false,
              FinalPrice = 4
            },
            new PizzasModel()
            {
              ImageTitle = "Clazone",
              PizzaName = "Clazone",
              BasePrice = 2,
              TomatoSauce = true,
              Cheese = true,
              Beef = true,
              Ham = true,
              Mushroom = true,
              Pepperoni = false,
              Pineapple = false,
              Tuna = true,
              FinalPrice = 7
            },
            new PizzasModel()
            {
              ImageTitle = "Diablo",
              PizzaName = "Diablo",
              BasePrice = 2,
              TomatoSauce = true,
              Cheese = true,
              Beef = true,
              Ham = true,
              Mushroom = false,
              Pepperoni = false,
              Pineapple = true,
              Tuna = false,
              FinalPrice = 17
            },
            new PizzasModel()
            {
              ImageTitle = "Hawaii",
              PizzaName = "Hawaii",
              BasePrice = 2,
              TomatoSauce = true,
              Cheese = true,
              Beef = true,
              Ham = false,
              Mushroom = true,
              Pepperoni = true,
              Pineapple = false,
              Tuna = true,
              FinalPrice = 8
            },
            new PizzasModel()
            {
              ImageTitle = "Marinara",
              PizzaName = "Marinara",
              BasePrice = 2,
              TomatoSauce = true,
              Cheese = true,
              Beef = true,
              Ham = false,
              Mushroom = false,
              Pepperoni = true,
              Pineapple = false,
              Tuna = false,
              FinalPrice = 5
            },
            new PizzasModel()
            {
              ImageTitle = "Mushrooms",
              PizzaName = "Mushrooms",
              BasePrice = 2,
              TomatoSauce = true,
              Cheese = true,
              Beef = false,
              Ham = false,
              Mushroom = true,
              Pepperoni = true,
              Pineapple = true,
              Tuna = false,
              FinalPrice = 16
            },
            new PizzasModel()
            {
              ImageTitle = "Pepperoni",
              PizzaName = "Pepperoni",
              BasePrice = 2,
              TomatoSauce = false,
              Cheese = false,
              Beef = false,
              Ham = false,
              Mushroom = false,
              Pepperoni = true,
              Pineapple = false,
              Tuna = false,
              FinalPrice = 3
            },
            new PizzasModel()
            {
              ImageTitle = "Prosciutto",
              PizzaName = "Prosciutto",
              BasePrice = 2,
              TomatoSauce = true,
              Cheese = true,
              Beef = false,
              Ham = false,
              Mushroom = false,
              Pepperoni = false,
              Pineapple = false,
              Tuna = true,
              FinalPrice = 5
            },
            new PizzasModel()
            {
              ImageTitle = "QuattroFormaggi",
              PizzaName = "QuattroFormaggi",
              BasePrice = 2,
              TomatoSauce = true,
              Cheese = true,
              Beef = true,
              Ham = true,
              Mushroom = true,
              Pepperoni = true,
              Pineapple = false,
              Tuna = true,
              FinalPrice = 9
            },
        };
        public void OnGet()
        {
        }
    }
}
