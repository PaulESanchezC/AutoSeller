namespace Models.EnumModels;

public class Colors
{
    public string Value { get; private set; }

    public Colors(string value)
    {
        Value = value;
    }

    public static string White => new("White");
    public static string Black => new("Black");
    public static string Gray => new("Gray");
    public static string Silver => new("Silver");
    public static string Red => new("Red");
    public static string Blue => new("Blue");
    public static string Brown => new("Brown");
    public static string Green => new("Green");
    public static string Beige => new("Beige");
    public static string Gold => new("Gold");
    public static string Yellow => new("Yellow");
    public static string Purple => new("Purple");
    public static IEnumerable<string> ColorsList => new List<string>
    {
        Colors.White, Colors.Black, Colors.Gray, Colors.Silver, Colors.Red,
        Colors.Blue, Colors.Brown, Colors.Green, Colors.Beige, Colors.Gold,
        Colors.Yellow, Colors.Purple
    };

}


