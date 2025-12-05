using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceasar.Services.Parameters;

public enum EncryptionOperation
{
    Encrypt,
    Decrypt
}

public class EncryptionCommand
{
    public EncryptionOperation Operation { get; set; } = EncryptionOperation.Encrypt;
    public string InputText { get; set; } = string.Empty;
    public int Shift { get; set; } = 0;
}

public class EncryptionResult
{
    public string OutputText { get; set; } = string.Empty;
}