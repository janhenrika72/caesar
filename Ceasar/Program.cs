using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ceasar;
using Ceasar.Services.Interfaces;

// Build the host

var host = Host.CreateDefaultBuilder(args);

var serviceProvider = host.ConfigureServices(
    services => {
        services.AddEncryptionServices();
    }
);

var app = host.Build();

using var scope = app.Services.CreateScope();


// Execute the encryption/decryption

var caesarService = scope.ServiceProvider.GetRequiredService<ICryptoService>();

var encryptResult = caesarService.TransformFromArguments(args);

if (!encryptResult.IsSuccess)
{
    Console.Error.WriteLine("Transformation command could not be completed, due to following errors:");
    foreach (var error in encryptResult.Errors)
    {
        Console.Error.WriteLine($"- {error}");
    }
    Environment.ExitCode = 1;
    return;
}

Console.WriteLine(encryptResult.OutputText);