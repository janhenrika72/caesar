using Ceasar.Services.Interfaces;
using Ceasar.Services.Parameters;
using System.Text;

namespace Ceasar.Services;

public class CaesarCryptoService() : ICryptoService
{
    public EncryptionResult TransformFromArguments(string[] args)
    {
        // Parse arguments

        var parsedArguments = ParseArguments(args);

        if (!parsedArguments.IsSuccess)
        {
            return new EncryptionResult
            {
                IsSuccess = false,
                Errors = parsedArguments.Errors
            };
        }

        // Read input file

        var fileReadResult = ReadInputFile(parsedArguments.InputFilePath);

        if (!fileReadResult.IsSuccess)
        {
            return new EncryptionResult
            {
                IsSuccess = false,
                Errors = fileReadResult.Errors
            };
        }


        // Execute

        var encryptionCommand = new CaesarEncryptionCommand
        {
            Shift = parsedArguments.Shift,
            InputText = fileReadResult.FileContents,
            Operation = parsedArguments.IsDecryption ? EncryptionOperation.Decrypt : EncryptionOperation.Encrypt
        };

        return Transform(encryptionCommand);
    }

    public EncryptionResult Transform<T>(T command) where T : EncryptionCommand
    {
        var result = new EncryptionResult
        {
            IsSuccess = true,
            Errors = new List<string>()
        };

        var caesarEncryptionCommand = command as CaesarEncryptionCommand;
        if (caesarEncryptionCommand is null)
        {
            result.IsSuccess = false;
            result.Errors.Add("Invalid command type for Caesar encryption.");
            return result;
        }

        // Validate input

        var validationResult = ValidateCommand(caesarEncryptionCommand);

        if (!validationResult.IsValid)
        {
            result.IsSuccess = false;
            result.Errors.AddRange(validationResult.Errors);
            return result;
        }

        // Transform text

        var offset = command.Operation == EncryptionOperation.Decrypt ? -caesarEncryptionCommand.Shift : caesarEncryptionCommand.Shift;

        var outputText = new StringBuilder(command.InputText.Length);

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

            outputText.Append(Constants.Alphabet[newIndex]);
        }

        result.OutputText = outputText.ToString();

        return result;
    }

    private (bool IsSuccess, int Shift, string InputFilePath, bool IsDecryption, List<string> Errors) ParseArguments(string[] args)
    {
        var shiftArg = ArgumentsParser.GetRequiredValue<int>(args, 0);
        var inputFilePathArg = ArgumentsParser.GetRequiredValue<string>(args, 1);
        var isDecryptFlag = ArgumentsParser.GetOptionalFlag(args, 2, "-d");
        var errors = new List<string>();

        if (!shiftArg.IsValid)
        {
            errors.Add("Shift value not specified");
        }

        if (!inputFilePathArg.IsValid)
        {
            errors.Add("Input file path not specified");
        }

        if (errors.Count > 0)
        {
            return (false, 0, string.Empty, false, errors);
        }

        return (true, shiftArg.Value, inputFilePathArg.Value, isDecryptFlag, errors);
    }

    private (bool IsSuccess, string FileContents, List<string> Errors) ReadInputFile(string inputFilePath)
    {
        var errors = new List<string>();
        if (!System.IO.File.Exists(inputFilePath))
        {
            errors.Add($"Input file '{inputFilePath}' does not exist.");
            return (false, string.Empty, errors);
        }

        try
        {
            var fileContents = System.IO.File.ReadAllText(inputFilePath, Encoding.UTF8);
            return (true, fileContents, errors);
        }
        catch (Exception ex)
        {
            errors.Add($"Error reading input file '{inputFilePath}': {ex.Message}");
            return (false, string.Empty, errors);
        }
    }

    private (bool IsValid, List<string> Errors) ValidateCommand(CaesarEncryptionCommand command)
    {
        var errors = new List<string>();

        // Validate shift value

        if (command.Shift < 1 || command.Shift > Constants.Alphabet.Length)
        {
            errors.Add($"Shift value must be between 1 and {Constants.Alphabet.Length}.");
        }

        // Validate input text

        if (string.IsNullOrEmpty(command.InputText))
        {
            errors.Add("Input text cannot be empty.");
        }

        // Validate characters in input text

        foreach (char c in command.InputText)
        {
            if (!char.IsWhiteSpace(c) && !char.IsPunctuation(c) && !Constants.Alphabet.Contains(char.ToLowerInvariant(c)))
            {
                errors.Add($"Invalid character '{c}' in input text.");
            }
        }

        return (errors.Count == 0, errors);
    }
}