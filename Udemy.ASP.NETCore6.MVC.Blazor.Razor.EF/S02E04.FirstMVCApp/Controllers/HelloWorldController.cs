using Microsoft.AspNetCore.Mvc;
using S02E04.FirstMVCApp.Models;

namespace S02E04.FirstMVCApp.Controllers
{
    public class HelloWorldController : Controller
    {
        // the list MUST be "STATIC" or with every redirect to INDEX will be empty!!!
        private static List<DogViewModel> dogs = new List<DogViewModel>();

        public IActionResult Index()
        {
            return View(dogs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DogViewModel());
        }

        [HttpPost]
        public IActionResult Create(DogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dog = new DogViewModel
                {
                    Name = model.Name,
                    Age = model.Age
                };

                dogs.Add(dog);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
