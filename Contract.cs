/*
介面定義了一個契約：一組相關的方法、屬性、事件和索引器，這些必須由class或struct實作。 
介面允許單一型態實作多個合約，這很重要，因為 C# 不支援多個類別繼承。 
結構體不能繼承其他結構或類別，因此介面是唯一能在結構型別間加入共享行為的方式。


*/

var car1 = new Car { Make = "aaa", Model = "xxx", Year = "1999" };
var car2 = new Car { Make = "aaa", Model = "xxx", Year = "1999" };
var car3 = new Car { Make = "bbb", Model = "xxx", Year = "1999" };

Console.WriteLine($"car1 = car2 ? {car1.Equals(car2)}");
Console.WriteLine($"car1 = car3 ? {car1.Equals(car3)}");

var order = new OrderService(new ConsoleLogger());
var order1 = new OrderService(new FileLogger());

order.PlaceOrder();
order1.PlaceOrder();


var c1 = new Circle(2);
Console.WriteLine($"Area: {c1.Area}");
c1.Draw();

var i1 = new InternalConfiguration { Setting = "setting" };
Console.WriteLine($"i1.Setting: {i1.Setting}");
//Configure 是明確實作 → 類別表面上看不到，要透過介類型變數才能實作
var i2 = new ServiceImplementation();
i2.Log("sss");
((IConfigurable)i2).Configure(i1);

interface IEquatable<T>
{
    bool Equals(T obj);
}

public class Car : IEquatable<Car>
{
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? Year { get; set; }

    public bool Equals(Car? car) =>
         car is not null && (Make, Model, Year) == (car.Make, car.Model, car.Year);
}

//用關鍵字定義一個介面 interface 。 依慣例，介面名稱以大寫 I字母開頭：
public interface ILogger
{
    void Log(string message);
    string Name { get; }
}

public class ConsoleLogger : ILogger
{
    public string Name => "Console";
    public void Log(string message) =>
        Console.WriteLine($"[{Name}] {message}");
}

public class FileLogger : ILogger
{
    public string Name => "File";
    public void Log(string message)
    {
        Console.WriteLine($"[{Name}] Writing to file: {message}");
    }
}

public class OrderService
{
    private readonly ILogger _logger;
    public OrderService(ILogger logger) => _logger = logger;
    public void PlaceOrder() => _logger.Log("order placed");
}

/*

明確實現
有時你需要實作介面成員，但不讓它成為類別的公開 API。 
明確實作 則以介面名稱限定成員。 
該成員僅能透過介面類型的變數存取：
*/

interface IMetric
{
    double GetDistance(); // Returns meters
}

interface IImperial
{
    double GetDistance(); // Returns feet
}

public class Runway(double meters) : IMetric, IImperial
{
    // Explicit implementation for IMetric
    double IMetric.GetDistance() => meters;
    // Explicit implementation for IImperial
    double IImperial.GetDistance() => meters * 3.28084;
}

// interface 可以繼承其他 interface
interface IDrawable
{
    void Draw();
}

interface IShape : IDrawable
{
    double Area { get; }
}

public class Circle(double radius) : IShape
{
    public double Area => Math.PI * radius * radius;

    public void Draw() =>
        Console.WriteLine($"Drawing circle with area {Area:F2}");
}

// internal interface
internal class InternalConfiguration
{
    public string Setting { get; set; } = "";
}

internal interface ILoggable
{
    void Log(string message);
}

internal interface IConfigurable
{
    void Configure(InternalConfiguration config);
}

public class ServiceImplementation : ILoggable, IConfigurable
{
    public void Log(string message) =>
        Console.WriteLine($"Log: {message}");

    void IConfigurable.Configure(InternalConfiguration config)
    {
        Console.WriteLine($"Configured with: {config.Setting}");
    }
}