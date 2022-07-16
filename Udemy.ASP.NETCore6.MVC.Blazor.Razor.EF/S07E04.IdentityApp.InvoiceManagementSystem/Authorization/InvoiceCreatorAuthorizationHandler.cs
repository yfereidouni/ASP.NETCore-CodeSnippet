using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using S07E04.IdentityApp.InvoiceManagementSystem.Models;

namespace S07E04.IdentityApp.InvoiceManagementSystem.Authorization;

public class InvoiceCreatorAuthorizationHandler :
    AuthorizationHandler<OperationAuthorizationRequirement, Invoice>
{
    private readonly UserManager<IdentityUser> _userManager;

    public InvoiceCreatorAuthorizationHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, Invoice invoice)
    {
        if (context.User == null || invoice == null)
        {
            //For Dennied Permission
            return Task.CompletedTask;
        }

        if (requirement.Name != Constants.CreateOperationName &&
            requirement.Name != Constants.ReadOperationName &&
            requirement.Name != Constants.UpdateOperationName &&
            requirement.Name != Constants.DeleteOperationName)
        {
            //For Dennied Permission
            return Task.CompletedTask;
        }

        //For Grant Permission
        if (invoice.CreatorId == _userManager.GetUserId(context.User))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
