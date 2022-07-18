using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using S07E04.IdentityApp.Data;
using S07E04.IdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using S07E04.IdentityApp.Authorization;

namespace S07E04.IdentityApp.Pages.Invoices
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || Context.Invoices == null || Invoice == null)
            //{
            //    return Page();
            //}

            Invoice.CreatorId = UserManager.GetUserId(User);
            var isAuthorize = await AuthorizationService.AuthorizeAsync(
                User, Invoice, InvoiceOperations.Create);

            if (!isAuthorize.Succeeded)
                return Forbid();

            Context.Invoices.Add(Invoice);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
