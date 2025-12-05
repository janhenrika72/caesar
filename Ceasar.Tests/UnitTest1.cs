using Ceasar.Services;
using Ceasar.Services.Parameters;

namespace Ceasar.Tests;

public class EncryptionTests(CaesarCryptoService caesarCryptoService)
{
    [Fact]
    public void Given_SpaceInInput()
    {
        // Arrange

        var inputText = " ";
        var shift = 3;

        var command = new EncryptionCommand { InputText = inputText, Shift = shift, Operation = EncryptionOperation.Encrypt };

        // Act
        var result = caesarCryptoService.Transform(command);

        Assert.Equal(" ", result.OutputText);

    }
}