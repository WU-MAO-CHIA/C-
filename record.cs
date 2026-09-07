/*
record關鍵字是你對 a class 或 structa 施加的修飾符
使用記錄的時機
當以下條件全部成立時，請使用紀錄：

此類型的主要角色是儲存資料。
兩個值相同的實例應該相等。
你想要的是不可變性（尤其是針對record class型別）。
你想要的是自動生成的可讀 ToString，而不是手動去寫一個。

Learn.NETC#
C# 記錄類型
本文內容
使用記錄的時機
宣告一筆紀錄
record class 與 record struct
價值平等
顯示其他 4 個
 提示

剛開始開發軟體嗎？ 先從 入門 教學開始。 當你需要簡潔且內建等式的資料型態時，會遇到紀錄。

有其他語言的經驗嗎？ C# 記錄類似於 Kotlin 中的資料類別或 Scala 中的案例類別。 它們是為儲存資料而優化的類型，並具備編譯器產生的等值， ToString以及複製語意。 record class瀏覽 vs record struct 和 with expressions 區塊，找出 C# 專屬的模式。

record關鍵字是你對 a class 或 structa 施加的修飾符。 它告訴編譯器透過ToString表達式來產生值的相等性、格式化with和非破壞性變更。 底層型別（類別或結構體）仍決定實例是使用參考語意還是值語意。 修 record 飾符是在這些語意基礎上加入資料友善的行為。 當一個類型的主要角色是儲存資料，且兩個值相同的實例應視為相等時，使用記錄。

使用記錄的時機
當以下條件全部成立時，請使用紀錄：

此類型的主要角色是儲存資料。
兩個值相同的實例應該相等。
你想要的是不可變性（尤其是針對record class型別）。
你想要的是自動生成的可讀 ToString，而不是手動去寫一個。
在record class和record struct之間選擇時：

當你需要繼承，或者當類型夠大，以至於每次作業都要複製而成本高昂時，請使用 record class。
使用 record struct 於小型且自包含的值，當中複製語義和堆疊分配具有優勢時。

分配一個 record class 是拷貝參考。 兩個變數都指向同一個物件。 
指派 a record struct 會複製資料，因此對一個變數的變更不會影響另一個變數：

*/

Person p1 = new Person("Grace", "Hopper");
var p2  = p1;
Console.WriteLine(ReferenceEquals(p1, p2));

var z1 = new Product { Name = "apple", Price = 10.22M};
var z2 = z1;
Console.WriteLine(ReferenceEquals(z1, z2));
z2.Price = 99.00M;
Console.WriteLine(z1.Name, z1.Price);
Console.WriteLine(z2.Name, z2.Price);
Console.WriteLine(ReferenceEquals(z1, z2));

var c1 = new Coordinate(47.6062, -122.3321);
var c2 = c1;

Console.WriteLine(c1 == c2);
c2.Longitude = 0.0d;
Console.WriteLine(c1.Longitude);
Console.WriteLine(c2.Longitude);
Console.WriteLine(c1 == c2);

var phones = new string[] { "555-1234" };
var person1 = new Person2("Grace", "Hopper", phones);
var person2 = new Person2("Grace", "Hopper", phones);

Console.WriteLine(person1 == person2);
Console.WriteLine(ReferenceEquals(person1, person2));

person1.PhoneNumbers[0] = "555-9999";
Console.WriteLine(person2.PhoneNumbers[0]);
Console.WriteLine();

// use with to duplicate
var original = new Person("Grace", "Hopper");
var modified = original with { FirstName = "Margaret"};

Console.WriteLine(original); // Person { FirstName = Grace, LastName = Hopper }
Console.WriteLine(modified); // Person { FirstName = Margaret, LastName = Hopper }
Console.WriteLine(original == modified); // False

var copy = original with { };
Console.WriteLine(original == copy); // True

//deconsturct
var (first, last) = p1;
Console.WriteLine($"{first} {last}");

public record Person(string FirstName, string LastName); // equal to record class 
public record Person2(string FirstName, string LastName, string[] PhoneNumbers) : Person(FirstName, LastName);// equal to record class 
public record class Product
{
    public required string Name { get; init; }
    public decimal Price { get; set; }
}

public record struct Coordinate (double Latitude, double Longitude);
public readonly record struct Temperature(double Celsius)
{
    public double Fahrenheit => Celsius * 9.0 / 5.0 + 32.0;
}

// inherit only class inherit, struct can not
public record Student(string FirstName, string LastName, int GradeLevel) : Person(FirstName, LastName);
