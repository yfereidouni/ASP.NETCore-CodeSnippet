using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace S07E04.IdentityApp.InvoiceManagementSystem.Authorization;

public class InvoiceOperations
{
    public static OperationAuthorizationRequirement Create =
        new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };

    public static OperationAuthorizationRequirement Read =
        new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };

    public static OperationAuthorizationRequirement Update =
        new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

    public static OperationAuthorizationRequirement Delete =
        new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };


    public static OperationAuthorizationRequirement Approve =
        new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };

    public static OperationAuthorizationRequirement Reject =
        new OperationAuthorizationRequirement { Name = Constants.RejectOperationName };
}

public class Constants
{
    public static readonly string CreateOperationName = "Create";
    public static readonly string ReadOperationName = "Read";
    public static readonly string UpdateOperationName = "Update";
    public static readonly string DeleteOperationName = "Delete";

    public static readonly string ApproveOperationName = "Approve";
    public static readonly string RejectOperationName = "Reject";

    public static readonly string InvoiceManagersRole = "InvoiceManager";
    public static readonly string InvoiceAdminRole = "InvoiceAdmin";
}

