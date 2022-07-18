using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using S07E04.IdentityApp.Data;
using S07E04.IdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using S07E04.IdentityApp.Authorization;

namespace S07E04.IdentityApp.Pages.Invoices
{
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public Invoice Invoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || Context.Invoices == null)
            {
                return NotFound();
            }

            Invoice = await Context.Invoices.FirstOrDefaultAsync(m => m.InvoiceId == id);

            if (Invoice == null)
                return NotFound();

            var isCreator = await AuthorizationService.AuthorizeAsync(
                User, Invoice, InvoiceOperations.Read);

            var isManager = User.IsInRole(Constants.InvoiceManagersRole);

            if (!isCreator.Succeeded && !isManager)
                return Forbid();


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, InvoiceStatus status)
        {
            #region Toturial
            Invoice = await Context.Invoices.FindAsync(id);

            if (Invoice == null)
                return NotFound();

            var invoiceOperation = status == InvoiceStatus.Approved
                ? InvoiceOperations.Approve
                : InvoiceOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                User, Invoice, invoiceOperation);

            if (!isAuthorized.Succeeded)
                return Forbid();

            Invoice.Status = status;
            Context.Invoices.Update(Invoice);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
            #endregion

            #region My_handy_way_as_an_alternative
            //var invoice = await Context.Invoices.FirstOrDefaultAsync(m => m.InvoiceId == id);

            //invoice.Status = status;

            //Invoice = invoice;

            //Context.Attach(Invoice).State = EntityState.Modified;

            //try
            //{
            //    await Context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!InvoiceExists(Invoice.InvoiceId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return RedirectToPage("./Index");
            #endregion
        }

        private bool InvoiceExists(int id)
        {
            return (Context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
