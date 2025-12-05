using Ceasar;
using Ceasar.Services.Parameters;
using System.Text;

namespace Ceasar.Services;


public class CaesarCryptoService
{
    public EncryptionResult Transform(EncryptionCommand command)
    {
        var offset = command.Operation == EncryptionOperation.Decrypt ? -command.Shift : command.Shift;

        var outputText = new StringBuilder();

        foreach (char c in command.InputText)
        {
            if (char.IsWhiteSpace(c) || char.IsPunctuation(c))
            {
                outputText.Append(c);
                continue;
            }

            var index = Constants.Alphabet.IndexOf(c, StringComparison.OrdinalIgnoreCase);

            var newIndex = (index + offset) % Constants.Alphabet.Length;

            if (newIndex < 0)
            {
                newIndex = Constants.Alphabet.Length + newIndex;
            }

            var a = Constants.Alphabet[newIndex];


            outputText.Append(Constants.Alphabet[newIndex]);
        }

        return new EncryptionResult { OutputText = outputText.ToString() };
    }
}