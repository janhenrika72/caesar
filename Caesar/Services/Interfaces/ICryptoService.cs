using Caesar.Services.Parameters;

namespace Caesar.Services.Interfaces;
public interface ICryptoService
{
    EncryptionResult TransformFromArguments(string[] args);
    EncryptionResult Transform<T>(T command) where T : EncryptionCommand;
}

