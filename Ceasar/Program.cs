using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ceasar;
using Ceasar.Services;

// Build the host

var host = Host.CreateDefaultBuilder(args);

var serviceProvider = host.ConfigureServices(
    services => {
        services.AddServices();
    }
);

var app = host.Build();

using var scope = app.Services.CreateScope();

// Build the command from input arguments

var commandBuilder = scope.ServiceProvider.GetRequiredService<EncryptionCommandBuilder>();
var commandResult = commandBuilder.BuildFromArguments(args);

if (!commandResult.IsValid)
{
    Console.Error.WriteLine($"Error: {commandResult.ErrorMessage}");
    Environment.ExitCode = 1;
    return;
}

// Execute the encryption/decryption

var caesarService = scope.ServiceProvider.GetRequiredService<CaesarCryptoService>();
var encryptResult = caesarService.Transform(commandResult.Command!);

Console.WriteLine(encryptResult.OutputText);