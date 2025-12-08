using Caesar.Services.Interfaces;
using Caesar.Services.Parameters;

namespace Caesar.Tests;

public class CaesarCryptoServiceTests(ICryptoService caesarCryptoService)
{
    [Fact]
    public void Given_Valid_InputFile_Then_Returns_Encrypted_Text()
    {
        // Arrange
        var args = new string[]
        {
            "3",
            $"plaintext.txt" // Improvement: Don't assume file exists, write/delete temp file in test setup
        };

        // Act

        var result = caesarCryptoService.TransformFromArguments(args);

        // Assert
        Assert.True(result.IsSuccess);
        // Improvement: Use specific error code instead of string matching
        Assert.Empty(result.Errors);
        Assert.StartsWith("hæshulhqfh lv wkh", result.OutputText.Trim());
    }

    [Fact]
    public void Given_NonExisting_InputFile_Then_Returns_Error()
    {
        // Arrange
        var args = new string[]
        {
            "3",
            $"{Guid.NewGuid()}.txt"
        };

        // Act

        var result = caesarCryptoService.TransformFromArguments(args);

        // Assert
        Assert.False(result.IsSuccess);
        // Improvement: Use specific error code instead of string matching
        Assert.True(result.Errors.Any(e => e.Contains("Input file")), "Expected error about input file not found");
    }

    [Fact]
    public void Given_Space_In_Input_When_Encrypting__Then_Returns_Space()
    {
        // Arrange

        var command = new CaesarEncryptionCommand
        {
            Shift = 3,
            InputText = " ",
            Operation = EncryptionOperation.Encrypt
        };

        // Act

        var result = caesarCryptoService.Transform(command);

        Assert.Equal(" ", result.OutputText);

    }

    [Fact]
    public void Given_Characters_At_End_Of_Alphabet_When_Encrypting__Then_Wrap_Around()
    {
        // Arrange

        var command = new CaesarEncryptionCommand
        {
            Shift = 3,
            InputText = "æøå",
            Operation = EncryptionOperation.Encrypt
        };

        // Act

        var result = caesarCryptoService.Transform(command);
        Assert.Equal("abc", result.OutputText);
    }


    [Fact]
    public void Given_Characters_At_Beginning_Of_Alphabet_When_Decrypting__Then_Wrap_Around()
    {
        // Arrange

        var command = new CaesarEncryptionCommand
        {
            Shift = 3,
            InputText = "abc",
            Operation = EncryptionOperation.Decrypt
        };

        // Act

        var result = caesarCryptoService.Transform(command);
        Assert.Equal("æøå", result.OutputText);
    }

    [Fact]
    public void Given_Invalid_Shift_Value_Then_Returns_Error()
    {
        // Arrange
        var command = new CaesarEncryptionCommand
        {
            Shift = 0,
            InputText = "hello",
            Operation = EncryptionOperation.Encrypt
        };
        // Act
        var result = caesarCryptoService.Transform(command);
        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.Errors.Any(e => e.Contains("Shift value must be between")), "Expected error about invalid shift value");
    }

    // Improvement: Add specific tests for punctuation handling, case sensitivity
    // Improvement: Add specific test for argument parsing logic

}