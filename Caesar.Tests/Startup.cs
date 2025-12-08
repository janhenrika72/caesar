using Microsoft.Extensions.DependencyInjection;

namespace Caesar.Tests;
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEncryptionServices();
    }
}