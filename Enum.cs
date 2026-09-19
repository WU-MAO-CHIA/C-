var today = Season.Autumn;
Console.WriteLine(DescribeSeason(today));
// Console.WriteLine(DescribeSeason(today = Season.test));

var permissions = FileAccess.Execute | FileAccess.Write;
Console.WriteLine(permissions);                          // ReadWrite
Console.WriteLine((int)permissions);
Console.WriteLine(permissions.HasFlag(FileAccess.Read));
Console.WriteLine(permissions.HasFlag(FileAccess.Execute));

var acc = Access.Execute | Access.Write;
Console.WriteLine(acc);                          // ReadWrite
Console.WriteLine((int)acc);
Console.WriteLine((acc & Access.Write) != 0);
Console.WriteLine((acc & Access.Execute) != 0);

//在枚舉與整數之間轉換
var status = HttpStatus.NotFound;
ushort code = (ushort)status;
Console.WriteLine($"Status: {status} ({code})");
var fromcode = (HttpStatus)200;
Console.WriteLine(fromcode);

/*
解析字串與迭代值
Enum基底類別提供解析字串及遍歷所有定義值的方法：
*/

var parsed = Enum.Parse<Season>("Winter");
Console.WriteLine(parsed);

if (Enum.TryParse<Season>("Monsoon", out var unknown))
{
    Console.WriteLine(unknown);
}
else
{
    Console.WriteLine("'Monsoon' is not a valid Season");
}

// Iterate over all values in an enum:
foreach (var season in Enum.GetValues<Season>())
{
    Console.WriteLine($"{season} = {(int)season}");
}

//在交換表達式中使用枚舉
static string DescribeSeason(Season season) => season switch
{
    Season.Spring => "Flowers bloom and temperatures rise.",
    Season.Summer => "Long days and warm weather.",
    Season.Autumn => "Leaves change color and fall.",
    Season.Winter => "Short days and cold temperatures.",
    _ => throw new ArgumentOutOfRangeException(nameof(season))
};


enum Season
{
    Spring, //0
    Summer, //1
    Autumn, //2
    Winter, //3
    test
}

/*
當數字具有外部意義，例如HTTP狀態碼或協定識別碼時，請使用明確的值。 
底層型態可以是任何整數型別，唯獨char除外。 
使用byte、short、ushort、intuintlong或。ulong
*/
enum HttpStatus : ushort
{
    OK = 200,
    NotFound = 404,
    InternalServerError = 500,
}

[Flags] //[Flags] 是宣告這個 enum 是位元旗標 (bit field) —— 一個值可以同時代表「多個選項的組合」,而不是單一選項，ToString() / 字串格式化會拆解成名稱
//當枚舉代表多個選擇的組合而非單一選擇時，將每個成員定義為二的冪次方，然後應用 
//屬性 [Flags] 也會影響 ToString()。 它以逗號分隔的名稱（例如 Read, Write）顯示合併值，而
enum FileAccess
{
    None = 0, // 0000 0
    Read = 1, // 0001 1
    Write = 2, // 0010 2
    Execute = 4, // 0100 4
    ReadWrite = Read | Write, // 0011 3
    All = Read | Write | Execute // 0111 7
}


enum Access
{
    None = 0, // 0000 0
    Read = 1, // 0001 1
    Write = 2, // 0010 2
    Execute = 4, // 0100 4
    ReadWrite = Read | Write, // 0011 3
    All = Read | Write | Execute // 0111 7
}
