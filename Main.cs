// different Main Method
/*
static void Main() { }
static int Main() { }
static void Main(string[] args) { }
static int Main(string[] args) { }
static async Task Main() { }
static async Task<int> Main() { }
static async Task Main(string[] args) { }
static async Task<int> Main(string[] args) { }
*/

/// <summary>
/// sdfafasdf
/// </summary>
/// <param name="a">firts</param>
class MainReturnValTest
{
    /// <summary>
    /// 計算兩數相加
    /// </summary>
    /// <param name="a">第一個數字</param>
    /// <param name="b">第二個數字</param>
    /// <returns>兩數相加的結果</returns>
    public int Add(int a, int b)
    {
        return a + b;
    }

    static async Task<int> Main(string[] args)
    {
        long l = long.Parse(args[0]);
        Console.WriteLine($"Length of args: {args.Length}");
        var num = new MainReturnValTest().Add(1, 2);
        return await AsyncConsoleWork();
    }

    private static async Task<int> AsyncConsoleWork()
    {
        return 0;
    }
}

