using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using S07E04.IdentityApp.InvoiceManagementSystem.Authorization;
using S07E04.IdentityApp.InvoiceManagementSystem.Data;
using S07E04.IdentityApp.InvoiceManagementSystem.Models;

namespace S07E04.IdentityApp.InvoiceManagementSystem.Pages.Invoices
{
    //[AllowAnonymous]
    public class IndexModel : DI_BasePageModel
    {

        public IndexModel(ApplicationDbContext applicationDbContext,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(applicationDbContext, authorizationService, userManager)
        {
        }

        public IList<Invoice> Invoice { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var invoices = from i in Context.Invoices
                           select i;

            var isManager = User.IsInRole(Constants.InvoiceManagersRole);

            var currentUserId = UserManager.GetUserId(User);


            if (!isManager)
                invoices = invoices.Where(i => i.CreatorId == currentUserId);

            Invoice = await invoices.ToListAsync();
        }
    }
}
