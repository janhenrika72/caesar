using Ceasar.Services.Parameters;

namespace Ceasar.Services.Interfaces;
public interface ICryptoService
{
    EncryptionResult TransformFromArguments(string[] args);

    EncryptionResult Transform<T>(T command) where T : EncryptionCommand;
}

