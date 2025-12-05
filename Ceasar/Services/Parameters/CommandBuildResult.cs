using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceasar.Services.Parameters;
public class CommandBuildResult
{
    public bool IsValid { get; set; }
    public string? ErrorMessage { get; set; }
    public EncryptionCommand? Command { get; set; }
}
