namespace Microsoft.eShopWeb.Infrastructure.Services;

public class GenerateDateTimeCommand : IGenerateDateTimeCommand
{
    public DateTime Execute()
    {
        return DateTime.Now;
    }
}
