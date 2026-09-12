/*
Generics 讓你能寫出能在任何類型運作的程式碼，同時保持完整型別安全。 
與其為 、 int以及你需要的其他類型分別寫類別或方法string，不如寫一個版本，包含一個或多個型別參數（例如 T、 和 TKeyTValue），並在使用時指定實際型別。 
編譯器會在編譯時檢查類型，所以你不需要執行時的類型轉換或承擔風險 InvalidCastException。
The System.Collections.Generic namespace provides type-safe collection classes. 
Always use these collections instead of nongeneric collections like ArrayList:
*/



using System.Reflection.Metadata;

List<int> scores = [95, 87, 72, 91];
Dictionary<string, decimal> prices = new ()
{
    ["Widget"] = 19.99m,
    ["Gadget"] = 29.99m
};

Task<string> greeting = Task.FromResult("Hello, generics!");
Func<int, bool> isPositive = n => n > 0; // Func 可以當成參數傳給其他method

Console.WriteLine($"First score: {scores[0]}");
Console.WriteLine($"Widget price: {prices["Widget"]:C}");
Console.WriteLine($"Greeting: {await greeting}");
Console.WriteLine($"Is 5 positive? {isPositive(5)}");

// A strongly typed list of strings
List<string> names = ["Alice", "Bob", "Tom"];
names.Add("Cindy");
// names.Add(21); //Compile-time error: can't add an int to List<string>

// A dictionary mapping string keys to int values
var inventory = new Dictionary<string, int>
{
    ["Apples"] = 50,
    ["Oranges"] = 30
};
inventory["Bananas"] = 25;

// A set that prevents duplicates
HashSet<int> uniqueIds = [1, 2, 3, 1, 2];
Console.WriteLine($"Unique count : {uniqueIds.Count}"); //3

// A FIFO queue
Queue<string> tasks = new();
tasks.Enqueue("build");
tasks.Enqueue("Test");
Console.WriteLine($"Next task: {tasks.Dequeue()}"); // Build
Console.WriteLine($"Next task: {tasks.Dequeue()}"); // Test

/*
Generic methods
一般方法會宣告自己的型別參數。 編譯器通常會根據你傳遞的值 推導 型別參數，因此你不需要明確指定：
*/

static void Print<T>(T value) =>
    Console.WriteLine($"Value: {value}");

Print(42);        // Compiler infers T as int
Print("hello");   // Compiler infers T as string
Print(3.14);      // Compiler infers T as double    

/*
Collection expressions
集合表達式（C# 12）提供簡潔的語法來建立集合。 使用方括號代替建構呼叫或初始化器語法：
*/

// Create a list with a collection expression
List<string> fruits = ["Apple", "Lemon", "Blue berry"];

int[] numbers = [1, 2, 3, 5];

// Works with any supported collection type
IReadOnlyList<double> temperatures = [72.0, 68.5, 75.9];

Console.WriteLine($"Fruits: {string.Join(", ", fruits)}");
Console.WriteLine($"Numbers: {string.Join(", ", numbers)}");
Console.WriteLine($"Temps: {string.Join(", ", temperatures)}");

//The spread operator (..) inlines the elements of one collection into another, which is useful for combining sequences:
List<int> first = [1, 2, 3];
List<int> second = [4, 5, 6];

List<int> combined = [9, .. first, .. second]; // .. 可以把集合的元素打開
Console.WriteLine(string.Join(",", combined));

/*

Dictionary initialization
You can initialize dictionaries concisely with indexer initializers. 
This syntax uses square brackets to set key-value pairs:
*/

Dictionary<string, int> scores2 = new()
{
    ["Alice"] = 95,
    ["bob"] = 87,
    ["Carol"] = 92
};

foreach (var (name, score) in scores2)
{
    Console.WriteLine($"{name}: {score}");
}

//You can merge dictionaries by copying one and applying overrides:

Dictionary<string, int> defaults = new()
{
    ["Timeout"] = 30,
    ["Retries"] = 3
};

Dictionary<string, int> overrides = new()
{
    ["Timeout"] = 60
};

//Merge defaults and overrides into a new dictionary
Dictionary<string, int> config = new(defaults);
foreach (var (key, value) in overrides)
{
    config[key] = value;
}

Console.WriteLine($"Timeout: {config["Timeout"]}");  // 60
Console.WriteLine($"Retries: {config["Retries"]}");   // 3


/*
Type constraints
泛型條件約束(generic constraint)
Constraints restrict which type arguments a generic type or method accepts. 
Constraints let you call methods or access properties on the type parameter 
that wouldn't be available on object alone:
*/



static T Max<T>(T a, T b) where T: IComparable<T> =>
    a.CompareTo(b) >= 0 ? a : b;

Console.WriteLine(Max(3, 7));
Console.WriteLine(Max("apple", "banana")); // banana
// Console.WriteLine(Max(defaults, overrides));

static T CreateDefault<T>() where T : new() => new T();
var list = CreateDefault<List<int>>();
Console.WriteLine($"Empty list count: {list.Count}");


/*
Covariance and contravariance
Covariance and contravariance describe how generic types behave with inheritance. 
They determine whether you can use a more derived or less derived type argument 
than originally specified:
*/

// Covariance: IEnumerable<Dog> can be used as IEnumerable<Animal>
// because IEnumerable<out T> is covariant

// List<Dog> dogs = [new("Rex"), new("Buddy")];
// IEnumerable<Animal> animals = dogs; // Allowed because Dog derives from Animal

// foreach (var animal in animals)
// {
//     Console.WriteLine(animal.Name);
// }

// // Contravariance: Action<Animal> can be used as Action<Dog>
// // because Action<in T> is contravariant

// Action<Animal> printAnimal = a => Console.WriteLine($"Animal: {a.Name}");
// Action<Dog> printDog = printAnimal; // Allowed because any Animal handler can handle Dog

// printDog(new Dog("Spot"));


/*
Create your own generic types
You can define your own generic classes, structs, interfaces, and methods. 
The following example shows a simple generic linked list for illustration. 
In practice, use List<T> or another built-in collection:
*/

var list2 = new GenericList<int>();
for (var i = 0; i < 10; i++)
{
    list2.AddHead(i);
}

foreach (var node in list2)
{
    Console.Write($"{node} ");
}


public class GenericList<T>
{
    private class Node(T data)
    {
        public T Data { get; set; } = data;
        public Node? Next { get; set; }
    }

    private Node? head;

    public void AddHead(T data)
    {
        var node = new Node(data) {Next = head};
        head = node;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = head;
        while (current is not null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }
}


