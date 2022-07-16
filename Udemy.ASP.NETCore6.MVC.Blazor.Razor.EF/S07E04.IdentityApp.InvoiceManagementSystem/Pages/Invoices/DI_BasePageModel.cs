using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using S07E04.IdentityApp.InvoiceManagementSystem.Data;

namespace S07E04.IdentityApp.InvoiceManagementSystem.Pages.Invoices;

public class DI_BasePageModel:PageModel
{
    protected ApplicationDbContext Context { get; }
    protected IAuthorizationService AuthorizationService { get; }
    protected UserManager<IdentityUser> UserManager { get; }

    public DI_BasePageModel(ApplicationDbContext applicationDbContext,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
    {
        Context = applicationDbContext;
        AuthorizationService = authorizationService;
        UserManager = userManager;
    }
}
