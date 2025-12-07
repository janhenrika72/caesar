using Ceasar.Services;
using Ceasar.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ceasar;
public static class ConfigureServices
{
    public static IServiceCollection AddEncryptionServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ICryptoService, CaesarCryptoService>()
        ;
    }
}
