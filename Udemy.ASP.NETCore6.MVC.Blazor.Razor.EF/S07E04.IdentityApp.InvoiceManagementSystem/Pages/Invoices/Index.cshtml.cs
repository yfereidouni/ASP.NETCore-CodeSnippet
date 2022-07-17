using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using S07E04.IdentityApp.InvoiceManagementSystem.Data;
using S07E04.IdentityApp.InvoiceManagementSystem.Models;

namespace S07E04.IdentityApp.InvoiceManagementSystem.Pages.Invoices
{
    [AllowAnonymous]
    public class IndexModel : DI_BasePageModel
    {

        public IndexModel(ApplicationDbContext applicationDbContext,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(applicationDbContext, authorizationService, userManager)
        {
        }

        public IList<Invoice> Invoice { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var currentUserId = UserManager.GetUserId(User);

            if (Context.Invoices != null)
            {
                Invoice = await Context.Invoices
                    .Where(c=>c.CreatorId == currentUserId)
                    .ToListAsync();
            }
        }
    }
}
