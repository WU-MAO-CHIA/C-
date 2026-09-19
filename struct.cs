/*
何時使用「struct」
請在以下情況使用結構體：

代表單一值或一小組相關值（約 16 位元組或更短）。
具有值語義——具有相同資料的兩個實例必須相等。
主要是資料容器，而非行為模型。
不需要從基底型別繼承（結構體不能繼承其他結構體或類別，但可以實作介面）。
*/

var p1 = new Point { X = 3, Y = 4 };
var p2 = p1 with { X = 10 };
Console.WriteLine(p1); // (3, 4)  — p1 is unchanged
Console.WriteLine(p2); // (10, 4) — only p2 was modified
Console.WriteLine(p1.ToString());
Console.WriteLine(p1.DistanceTo(p2));

var custom = new ConnectionSettings();
Console.WriteLine($"{custom.Host}:{custom.Port} (retries: {custom.MaxRetries})");

var defaults = default(ConnectionSettings);
Console.WriteLine($"{defaults.Host}:{defaults.Port} (retries: {defaults.MaxRetries})");

var tile = new GameTile(2, 5);
Console.WriteLine($"Tile ({tile.Row}, {tile.Column}), blocked: {tile.IsBlocked}");

var temp = new Temperature(100);
Console.WriteLine(temp);

var v = new Velocity { X = 3, Y = 4 };
Console.WriteLine(v.Speed); // 5
Console.WriteLine(v);       // (3, 4) speed=5.00
v.X = 6;
Console.WriteLine(v.Speed); // 7.211...
Console.WriteLine(v);

struct Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public readonly double DistanceTo(Point other)
    {
        var dx = X - other.X;
        var dy = Y - other.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public override string ToString() => $"({X}, {Y})";
}

struct ConnectionSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public int MaxRetries { get; set; }

    public ConnectionSettings()
    {
        Host = "localhost";
        Port = 8080;
        MaxRetries = 3;
    }
}

//編譯器會自動初始化你在建構子中未明確設定的欄位。 你只能初始化需要非預設值的欄位：
struct GameTile
{
    public int Row { get; set; }
    public int Column { get; set; }
    public bool IsBlocked { get; set; }

    public GameTile(int row, int column)
    {
        Row = row;
        Column = column;
        // IsBlocked is automatically initialized to false
    }
}

//readonly 

readonly struct Temperature
{
    public double Celsius { get; }
    public Temperature(double celsius) => Celsius = celsius;
    public double Fahrenheit => Celsius * 9.0 / 5.0 + 32.0;
    public override string ToString() => $"{Celsius:F1}°C ({Fahrenheit:F1}°F)";
}

struct Velocity
{
    public double X
    {
        readonly get;
        set;
    }

    public double Y
    {
        readonly get;
        set;
    }

    public readonly double Speed => Math.Sqrt(X * X + Y * Y);
    public readonly override string ToString() => $"({X}, {Y}) speed={Speed:F2}";
}

