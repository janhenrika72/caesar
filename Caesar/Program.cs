using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Caesar;
using Caesar.Services.Interfaces;

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

var cryptoService = scope.ServiceProvider.GetRequiredService<ICryptoService>();

var encryptionResult = cryptoService.TransformFromArguments(args);

if (!encryptionResult.IsSuccess)
{
    Console.Error.WriteLine("Transformation command could not be completed, due to following errors:");
    foreach (var error in encryptionResult.Errors)
    {
        Console.Error.WriteLine($"- {error}");
    }
    Environment.ExitCode = 1;
    return;
}

// Present
// Improvement: Could be further abstracted to output to file, etc.

Console.WriteLine(encryptionResult.OutputText);