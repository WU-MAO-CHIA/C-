using System.IO.Compression;
using System.Text;
using Microsoft.VisualBasic;

MyClass.TestMethod();
MyNamespace.MyClass.MyMethod();

Console.Write("Hello");
await Task.Delay(500);
Console.WriteLine("World!");

string? s = Console.ReadLine();
int returnValue = int.TryParse(s, out int v) ? v: -1;
return  returnValue;

StringBuilder builder = new();
builder.AppendLine("The following arguments are passed");

if (args.Length > 0)
{
    foreach (var arg in args)
    {
        Console.WriteLine($"Argument={arg}");
    }
}
else
{
    Console.WriteLine("No arguments");
}

Console.WriteLine(builder.ToString());

return 0;


// 如果有類型定義或命名空間，要接再陳述句後面
public class MyClass
{
    public static void TestMethod()
    {
        Console.WriteLine("Hello World");
    }
}

namespace MyNamespace
{
    class MyClass
    {
        public static void MyMethod()
        {
            Console.WriteLine("Hello World from MyNamespace.MyClass.MyMethod!");
        }
    }
}
