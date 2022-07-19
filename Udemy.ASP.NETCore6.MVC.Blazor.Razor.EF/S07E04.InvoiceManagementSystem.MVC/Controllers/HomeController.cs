using Microsoft.AspNetCore.Mvc;
using S07E04.InvoiceManagementSystem.MVC.Data;
using S07E04.InvoiceManagementSystem.MVC.Models;
using System.Diagnostics;

namespace S07E04.InvoiceManagementSystem.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var revenue = new Revenue();

            var invoices = _context.Invoices.ToList();
            foreach (var invoice in invoices)
            {
                switch (invoice.Status)
                {
                    case Models.InvoiceStatus.Submitted:
                        revenue.revenueSubmitted[invoice.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    case Models.InvoiceStatus.Approved:
                        revenue.revenueApproved[invoice.InvoiceMonth] += (int)invoice.InvoiceAmount;

                        break;
                    case Models.InvoiceStatus.Rejected:
                        revenue.revenueRejected[invoice.InvoiceMonth] += (int)invoice.InvoiceAmount;
                        break;
                    default:
                        break;
                }
            }

            return View(revenue);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}