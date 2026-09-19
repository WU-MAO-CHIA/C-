var customer = new Customer("Allison");
Console.WriteLine(customer.Name);

var c1 = new Customer("Grace");
var c2 = c1;

c2.Name = "Hopper";
Console.WriteLine(c1.Name);

var c3 = new Container(2);
c3.Print();


var person = new Person { FirstName = "Grace", LastName = "aaa" };
// var person1 = new Person { LastName = "aaa"};

Console.WriteLine($"{person.FirstName} {person.LastName}");

double circumference = MathHelpers.CircleCircumference(5.0);
Console.WriteLine($"Circumference: {circumference:F5}");

var options = new ConnectionOptions
{
    Host = "db.example.com",
    Port = 1234,
    UseSsl = true
};
Console.WriteLine($"{options.Host}:{options.Port} (SSL: {options.UseSsl})");

var options2 = new ConnectionOptions
{
    Host = "db.example.com",
    Port = 4321
};
Console.WriteLine($"{options2.Host}:{options2.Port} (SSL: {options2.UseSsl})");

public class Customer
{
    public string Name { get; set; }

    public Customer(string name) => Name = name;
}

public class Container(int capacity)
{
    private int _capacity = capacity;

    // 實例方法：透過 this 存取自己的 _capacity（this 可省略）
    public void Print()
    {
        Console.WriteLine($"capacity = {_capacity}");
    }
}

//必輸入的 attribute 在宣告時加入 required
public class Person
{
    public required string FirstName { get; set; }
    public string LastName { get; set; }
}

static class MathHelpers
{
    public static double CircleCircumference(double radius) =>
        2 * Math.PI * radius;
}

// 物件初始化器 讓你在建立物件時設定屬性，而不必為每個值組合寫建構子：
class ConnectionOptions
{
    public string Host { get; init; } = "localhost";
    public int Port { get; init; } = 80;
    public bool UseSsl { get; init; } = true;
}
