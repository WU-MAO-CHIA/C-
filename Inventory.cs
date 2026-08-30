namespace MyApp.Inventory;

public class InventoryService
{
    public int GetStockLevel(string productName) =>
        StockDatabase.Lookup(productName);
}

internal static class StockDatabase
{
    private static readonly Dictionary<string, int> _stock = new()
    {
        ["Widget"] = 42,
        ["Gadget"] = 17
    };

    internal static int Lookup(string productName) =>
            _stock.GetValueOrDefault(productName);
}

class Program
{
    static void Main(string[] atrgs)
    {
        InventoryService ser = new();
        var stock = ser.GetStockLevel("Widget");
        Console.WriteLine($"Widget : {stock}");
    }
}