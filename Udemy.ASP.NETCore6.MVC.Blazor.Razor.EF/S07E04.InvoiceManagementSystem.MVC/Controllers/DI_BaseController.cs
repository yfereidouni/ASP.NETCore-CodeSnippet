using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using S07E04.InvoiceManagementSystem.MVC.Data;

namespace S07E04.InvoiceManagementSystem.MVC.Controllers;

public class DI_BaseController : Controller
{
    protected ApplicationDbContext Context { get; }
    protected IAuthorizationService AuthorizationService { get; }
    protected UserManager<IdentityUser> UserManager { get; }

    public DI_BaseController(ApplicationDbContext applicationDbContext,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
    {
        Context = applicationDbContext;
        AuthorizationService = authorizationService;
        UserManager = userManager;
    }
}
