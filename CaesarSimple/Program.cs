using System.Text;

if (args.Length < 2)
{
    Console.WriteLine("Usage: Caesar <shift> <inputFile> [-d]");
    return;
}

var shift = int.Parse(args[0]);
var inputFile = args[1];
bool decrypt = args.Contains("-d");

if (!File.Exists(inputFile))
{
    Console.WriteLine($"Input file '{inputFile}' does not exist.");
    return;
}

var inputText = File.ReadAllText(inputFile);

const string alphabet = "abcdefghijklmnopqrstuvwxyzæøå";

if (shift < 1 || shift >= alphabet.Length)
{
    Console.WriteLine($"Shift value must be between 1 and {alphabet.Length}.");
    return;
}

var offset = decrypt ? -shift : shift;

var outputText = new StringBuilder(inputText.Length);

foreach (char c in inputText)
{
    if (char.IsWhiteSpace(c) || char.IsPunctuation(c))
    {
        outputText.Append(c);
        continue;
    }

    var index = alphabet.IndexOf(c, StringComparison.OrdinalIgnoreCase);

    var newIndex = (index + offset) % alphabet.Length;

    if (newIndex < 0)
    {
        newIndex = alphabet.Length + newIndex;
    }

    outputText.Append(alphabet[newIndex]);
}

Console.WriteLine(outputText);
