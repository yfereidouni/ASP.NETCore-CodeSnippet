using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using S07E04.IdentityApp.InvoiceManagementSystem.Authorization;
using S07E04.IdentityApp.InvoiceManagementSystem.Data;
using S07E04.IdentityApp.InvoiceManagementSystem.Models;

namespace S07E04.IdentityApp.InvoiceManagementSystem.Pages.Invoices
{
    public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(ApplicationDbContext applicationDbContext,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(applicationDbContext, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || Context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await Context.Invoices.FirstOrDefaultAsync(m => m.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }
            else
            {
                Invoice = invoice;
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Invoice, InvoiceOperations.Delete);

            if (!isAuthorized.Succeeded)
                return Forbid();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || Context.Invoices == null)
                return NotFound();
            
            var invoice = await Context.Invoices.FindAsync(id);

            if (invoice is null)
                NotFound();

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Invoice, InvoiceOperations.Delete);

            if (!isAuthorized.Succeeded)
                return Forbid();

            Invoice = invoice;
            Context.Invoices.Remove(Invoice);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
