using Microsoft.Extensions.DependencyInjection;

namespace Ceasar.Tests;
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddServices();
    }
}