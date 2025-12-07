using Ceasar.Services.Interfaces;
using Ceasar.Services.Parameters;

namespace Ceasar.Tests;

public class CaesarCryptoServiceTests(ICryptoService caesarCryptoService)
{
    [Fact]
    public void Given_SpaceInInput_When_Encrypting_Then_Returns_Space()
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
    public void Given_Characters_At_End_Of_Alphabet__Should_Wrap_Around()
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
    public void Given_SpaceInInput_When_Decrypting_Then_Returns_Space()
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
}