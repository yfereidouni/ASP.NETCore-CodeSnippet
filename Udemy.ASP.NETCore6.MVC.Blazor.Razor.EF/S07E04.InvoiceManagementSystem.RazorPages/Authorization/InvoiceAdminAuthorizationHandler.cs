using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using S07E04.IdentityApp.InvoiceManagementSystem.Models;

namespace S07E04.IdentityApp.InvoiceManagementSystem.Authorization;

public class InvoiceAdminAuthorizationHandler
    : AuthorizationHandler<OperationAuthorizationRequirement, Invoice>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        OperationAuthorizationRequirement requirement, 
        Invoice invoice)
    {
        if (context.User == null || invoice == null)
            return Task.CompletedTask;

        if (context.User.IsInRole(Constants.InvoiceAdminRole))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
