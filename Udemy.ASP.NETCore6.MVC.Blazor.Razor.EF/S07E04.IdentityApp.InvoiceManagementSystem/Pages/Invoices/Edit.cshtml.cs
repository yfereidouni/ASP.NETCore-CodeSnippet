using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using S07E04.IdentityApp.InvoiceManagementSystem.Authorization;
using S07E04.IdentityApp.InvoiceManagementSystem.Data;
using S07E04.IdentityApp.InvoiceManagementSystem.Models;

namespace S07E04.IdentityApp.InvoiceManagementSystem.Pages.Invoices
{
    public class EditModel : DI_BasePageModel
    {
        public EditModel(ApplicationDbContext applicationDbContext,
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
            Invoice = invoice;


            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Invoice, InvoiceOperations.Update);

            if (!isAuthorized.Succeeded)
                return Forbid();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            ///ModeState is not valid because CreatorId was not exist in the Front-End
            //if (!ModelState.IsValid)
            //    return Page();

            var invoice = await Context.Invoices.AsNoTracking()
                .SingleOrDefaultAsync(m => m.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            Invoice.CreatorId = invoice.CreatorId;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User,
                Invoice, InvoiceOperations.Update);

            if (!isAuthorized.Succeeded)
                return Forbid();

            Context.Attach(Invoice).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(Invoice.InvoiceId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InvoiceExists(int id)
        {
            return (Context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
