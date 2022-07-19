namespace S07E04.InvoiceManagementSystem.MVC.Models;

public class Revenue
{
    public Dictionary<string, int> revenueSubmitted = InitDict();
    public Dictionary<string, int> revenueApproved = InitDict();
    public Dictionary<string, int> revenueRejected = InitDict();

    private static Dictionary<string, int> InitDict()
    {
        return new Dictionary<string, int>()
            {
                {"January",0 },
                {"February",0},
                {"March",0},
                {"April",0 },
                {"May",0 },
                {"June",0 },
                {"July",0 },
                {"August",0 },
                {"September",0 },
                {"October",0 },
                {"November",0 },
                {"December",0 },
            };
    }
}
