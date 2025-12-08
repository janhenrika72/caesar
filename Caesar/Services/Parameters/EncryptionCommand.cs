using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar.Services.Parameters;
public class EncryptionCommand
{
    public string InputText { get; set; } = string.Empty;
    public EncryptionOperation Operation { get; set; } = EncryptionOperation.Encrypt;
}

public enum EncryptionOperation
{
    Encrypt,
    Decrypt
}


public class EncryptionResult
{
    public bool IsSuccess { get; set; } = true;

    public List<string> Errors { get; set; } = [];

    public string OutputText { get; set; } = string.Empty;
}
