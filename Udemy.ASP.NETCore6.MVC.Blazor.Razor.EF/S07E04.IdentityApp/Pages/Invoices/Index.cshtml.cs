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
using S07E04.IdentityApp.Models;
using S07E04.IdentityApp.Data;

namespace S07E04.IdentityApp.Pages.Invoices
{
    [AllowAnonymous]
    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Invoice> Invoice { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var invoices = from i in Context.Invoices
                           select i;


            var isManager = User.IsInRole(Constants.InvoiceManagersRole);

            var isAdmin = User.IsInRole(Constants.InvoiceAdminRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isManager && !isAdmin)
            {
                invoices = invoices.Where(c => c.CreatorId == currentUserId);
            }

            Invoice = await invoices.ToListAsync();
        }
    }
}
