using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPizzeria.Models;

namespace RazorPizzeria.Pages.Forms
{
    public class CustomPizzaModel : PageModel
    {
        [BindProperty]
        public PizzasModel Pizzas { get; set; }

        public float PizzaPrice { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            PizzaPrice = Pizzas.BasePrice;
            if (Pizzas.TomatoSauce) PizzaPrice += 1;
            if (Pizzas.Cheese) PizzaPrice += 1;
            if (Pizzas.Pepperoni) PizzaPrice += 1;
            if (Pizzas.Mushroom) PizzaPrice += 1;
            if (Pizzas.Tuna) PizzaPrice += 1;
            if (Pizzas.Pineapple) PizzaPrice += 10;
            if (Pizzas.Ham) PizzaPrice += 1;
            if (Pizzas.Beef) PizzaPrice += 1;

            return RedirectToPage("/Checkout/Checkout", new { Pizzas.PizzaName, PizzaPrice });
        }
    }
}
