//數字分隔符可以放在 _ 數字內的任何位置
int population = 67_000_000;
float gravity = 9.81f;
decimal price = 19.99m;
double pi = 4.34324d;

Console.WriteLine($"population: {population}");
Console.WriteLine($"gravity: {gravity}");
Console.WriteLine($"price: {price}");
Console.WriteLine($"pi: {pi}");

//無符號類型
//每個有符號整數類型都有一個無符號對應類型，該類型僅儲存非負值，其範圍是其正值範圍的兩倍。

uint filesize = 4_294_967_295;
ulong totalBytes = ulong.MaxValue;
ushort port = ushort.MaxValue;

Console.WriteLine($"filesize: {filesize}");
Console.WriteLine($"totalBytes: {totalBytes}");
Console.WriteLine($"port: {port}");

char newline = '\n';
char unicode = '\u0041'; //A
string message = $"Found {pi} items";
string path = @"C:\Users\docs\file.txt"; // 有@就不會\n 就不會判斷成換行符號，但新版的不加也不會除非有"""開頭
string json = """
    { "name": "Alice", "age": 30 }
    """;
string raw = $"""
    Found {gravity} items in "{path}"
    """;

Console.WriteLine($"unicode: {unicode}");
Console.WriteLine($"totalBytes: {path} {unicode} 111");

//default value
int defaultInt = default;
bool defaultBool = default;
string? defaultString = default;
double defaultdouble = default;

// Use default in a conditional:
var limit = (args.Length > 0) ? int.Parse(args[0]) : default(int);

Console.WriteLine($"defaultInt: {defaultInt}");
Console.WriteLine($"defaultBool: {defaultBool}");
Console.WriteLine($"defaultString: {defaultString}");
Console.WriteLine($"defaultdouble: {defaultdouble}");
Console.WriteLine($"limit: {limit}");

// var 會在編譯時被決定，並照常強制執行型別安全

// 當你已經從上下文中知道目標型別，例如變數宣告或方法參數時，你可以省略表達式中的 new 型別名稱

List<string> names = new List<string>() { "Alice", "Bob", "Charlie" };
List<string> sim = new() { "Alice", "Bob", "Charlie" };

Console.WriteLine($"names: {names.Count}");
Console.WriteLine($"sim: {sim.Count}");

// dynamic 型別可繞過編譯時型別檢查。 編譯器在執行時解析 dynamic 變數的操作。

dynamic value = 42;
Console.WriteLine(value.GetType());

value = "Now I'm a string";
Console.WriteLine(value.GetType());