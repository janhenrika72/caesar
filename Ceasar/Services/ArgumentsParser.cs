namespace Ceasar.Services;
public static class ArgumentsParser
{
    public static bool GetRequiredFlag(string[] args, int index, string argSwitch)
    {
        var argumentValue = GetValue<string>(args, index, string.Empty, false);

        return argumentValue.IsValid && argumentValue.Value.Equals(argSwitch, StringComparison.OrdinalIgnoreCase);
    }

    public static bool GetOptionalFlag(string[] args, int index, string argSwitch)
    {
        var argumentValue = GetValue<string>(args, index, string.Empty, true);

        return argumentValue.IsValid && argumentValue.Value.Equals(argSwitch, StringComparison.OrdinalIgnoreCase);
    }

    public static (bool IsValid, T Value) GetRequiredValue<T>(string[] args, int index)
    {
        return GetValue<T>(args, index, default(T)!, false);
    }

    public static (bool IsValid, T? Value) GetOptionalValue<T>(string[] args, int index, T defaultValue)
    {
        return GetValue<T>(args, index, defaultValue, true);
    }

    private static (bool IsValid, T Value) GetValue<T>(string[] args, int index, T defaultValue, bool isOptional)
    {
        if (args.Length > index)
        {
            try
            {
                return (true, (T)Convert.ChangeType(args[index], typeof(T)));
            }
            catch
            {
                return (isOptional, defaultValue);
            }
        }
        return (isOptional, defaultValue);
    }
}
