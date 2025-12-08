using Caesar.Services;
using Caesar.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Caesar;
public static class ConfigureServices
{
    public static IServiceCollection AddEncryptionServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ICryptoService, CaesarCryptoService>() // Improvement: Can add more ICryptoService implementations here, with keyed registrations
        ;
    }
}
