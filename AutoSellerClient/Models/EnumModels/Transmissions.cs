namespace Models.EnumModels;

public class Transmissions
{
    public string Value { get; private set; }

    public Transmissions(string value)
    {
        Value = value;
    }

    public static string Automatic => new("Automatic");
    public static string Manual => new("Manual");
    public static IEnumerable<string> TransmissionList => new List<string>
    { Transmissions.Automatic, Transmissions.Manual };
}

