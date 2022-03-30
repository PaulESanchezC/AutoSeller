namespace Models.EnumModels;

public class DriveTrains
{
    public string Value { get; private set; }

    public DriveTrains(string value)
    {
        Value = value;
    }

    public static string FWD => new("FWD");
    public static string RWD => new("RWD");
    public static string AWD => new("AWD");
    public static string FourByFour => new("4x4");

    public static IEnumerable<string> DriveTrainList => new List<string>
    { DriveTrains.FWD, DriveTrains.RWD,
            DriveTrains.AWD, DriveTrains.FourByFour
    };

}
