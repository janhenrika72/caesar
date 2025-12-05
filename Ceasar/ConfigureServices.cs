using Ceasar.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ceasar;
public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<CaesarCryptoService>()
            .AddSingleton<EncryptionCommandBuilder>()
        ;
    }
}
