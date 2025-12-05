using Ceasar.Services.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ceasar.Services;
public class EncryptionCommandBuilder
{
    public CommandBuildResult BuildFromArguments(string[] args)
    {
        var result = new CommandBuildResult() { IsValid = false };

        // Validate arguments

        if (args.Length < 2)
        {
            result.ErrorMessage = "Usage: Caesar <shift> <inputFile> [-d]";
            return result;
        }

        if (!int.TryParse(args[0], out int shift))
        {
            result.ErrorMessage = "Invalid shift value. Please provide a numeric shift value.";
            return result;
        }

        if (shift < 1 || shift > Constants.Alphabet.Length)
        {
            result.ErrorMessage = $"Shift value must be between 1 and {Constants.Alphabet.Length}.";
            return result;
        }

        string inputFilePath = args[1];
        if (!File.Exists(inputFilePath))
        {
            result.ErrorMessage = $"Input file '{inputFilePath}' does not exist.";
            return result;
        }

        string inputText = File.ReadAllText(inputFilePath, Encoding.UTF8);

        bool decrypt = args.Length > 2 && args[2].Equals("-d", StringComparison.OrdinalIgnoreCase);

        return BuildFromParameters(shift, inputText, decrypt);
    }

    public CommandBuildResult BuildFromParameters(int shift, string inputText, bool decrypt)
    {
        var result = new CommandBuildResult() { IsValid = true };

        // Validate input text

        foreach(var c in inputText)
        {
            if (char.IsDigit(c))
            {
                result.ErrorMessage = "Input text can only contain letters, spaces and punctuations";
                return result;
            }
        }

        result.Command = CreateEncryptCommand(shift, inputText, decrypt);
        return result;
    }

    public EncryptionCommand CreateEncryptCommand(int shift, string inputText, bool decrypt)
    {
        return new EncryptionCommand
        {
            Operation = decrypt ? EncryptionOperation.Decrypt : EncryptionOperation.Encrypt,
            InputText = inputText,
            Shift = shift
        };
    }
}
