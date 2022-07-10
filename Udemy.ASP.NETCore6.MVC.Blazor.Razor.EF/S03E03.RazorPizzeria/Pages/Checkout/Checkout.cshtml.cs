using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPizzeria.Data;
using RazorPizzeria.Models;

namespace RazorPizzeria.Pages.Checkout
{
    [BindProperties(SupportsGet = true)]
    public class CheckoutModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public CheckoutModel(ApplicationDbContext context)
        {
            _context = context;
        }

        //[BindProperty]
        public string PizzaName { get; set; }
        //[BindProperty]
        public float PizzaPrice { get; set; }
        //[BindProperty]
        public string ImageTitle { get; set; }
        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(PizzaName))
                PizzaName = "Custom";

            if (string.IsNullOrWhiteSpace(ImageTitle))
                ImageTitle = "custom-pizza1";

            //PizzaOrder pizzaOrder = new PizzaOrder();
            //pizzaOrder.PizzaName = PizzaName;
            //pizzaOrder.BasePrice = PizzaPrice;
            //_context.PizzaOrders.Add(pizzaOrder);
            //_context.SaveChanges();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(PizzaName))
                PizzaName = "Custom";

            PizzaOrder pizzaOrder = new PizzaOrder();
            pizzaOrder.PizzaName = PizzaName;
            pizzaOrder.BasePrice = PizzaPrice;
            _context.PizzaOrders.Add(pizzaOrder);
            _context.SaveChanges();


            return RedirectToPage("ThankYou");
        }
    }
}
