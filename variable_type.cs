int a = 5;
int b = a + 2;

bool test = true;

// Console.WriteLine(test + b); // error occur during the compile

int count = 10;
// double temperature = 36.6;

var name = "C#";
// var items = new List<string> {"one", "two", count};

// Console.WriteLine(GetGreeting(name, 1));

// Value type: each variable holds its own copy
var point1 = new Coords(3, 4);
var point2 = point1;

Console.WriteLine($"point1: ({point1.X}, {point1.Y})");
Console.WriteLine($"point1: ({point2.X}, {point2.Y})");
// point1 and point2 are independent copies

point2 = point2 with { Y = 6 };
// point2.Y = 6; 因為是 readonly 所以不能這樣改值
Console.WriteLine($"point1: ({point1.X}, {point1.Y})");
Console.WriteLine($"point1: ({point2.X}, {point2.Y})");

// Reference type: both variables refer to the same object
var list1 = new List<int> { 1, 2, 3 };
var list2 = list1;
list2.Add(5);
Console.WriteLine($"list1 count: {list1.Count}");
Console.WriteLine($"list2 count: {list2.Count}");

// Compile-time and run-time types match:
string message = "Hello, World!";
// Compile-time type differs from run-time type:
object boxed = "This is a string at run time";
IEnumerable<char> characters = "abcdefghijklmnopqrstuvwxyz";


static string GetGreeting(string name, int visitCount)
{
    return visitCount switch
    {
        1 => $"Welcome, {name}",
        _ => $"Welcome back, {name}! Visit #{visitCount}"
    };
}

/*
值類型 直接持有其資料。 當你為新變數指派值型別時，執行時會複製資料。 一個變數的變動不會影響另一個。 結構、列舉以及內建的數值型別都是值型別。

參考型別 會保留對受管理堆積物件的參考。 當你為新變數指派參考型別時，兩個變數都會指向同一個物件。 透過一個變數的變化，另一個變數的變化即可見。 類別、陣列、代理和字串都是參考型別。
*/

public readonly record struct Coords(int X, int Y);
