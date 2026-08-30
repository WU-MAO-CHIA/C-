// Console.WriteLine("Hello, world!");

namespace MySpace
{
    class MyClass
    {
        
    }

    struct MyStruct
    {
        
    }

    interface MyInterface
    {
        
    }

    delegate int MyDelegate();

    enum MyEnum
    {
        
    }

    class Program
    {
        static string aFriend = "Bukk";
        static string bFriend = "aill";
        static string greeting = "      Hello World!       ";

        static void Main(string[] args)
        {
            string songLyrics = "You say goodbye, and I say hello";
            Console.WriteLine("Traditional hello, world");
            Console.WriteLine("Hello" + aFriend);
            Console.WriteLine($"Hello {aFriend} and {bFriend}");
            Console.WriteLine($"The name {aFriend} has {aFriend.Length} letters");
            Console.WriteLine($"The name {bFriend} has {bFriend.Length} letters");
            Console.WriteLine($"[{greeting}]");
            Console.WriteLine($"[{greeting.TrimStart()}]");
            Console.WriteLine($"[{greeting.TrimEnd()}]");
            Console.WriteLine($"[{greeting.Trim()}]");
            Console.WriteLine($"{greeting.Trim().Replace("Hello", "Greetings")}");
            Console.WriteLine($"{greeting.ToUpper()}");
            Console.WriteLine($"{greeting.ToLower()}");
            Console.WriteLine($"{greeting.Contains("1234")}");
            Console.WriteLine($"{greeting.Contains("Hello")}");
            Console.WriteLine($"{songLyrics.StartsWith("You")}");
            Console.WriteLine($"{greeting.StartsWith("goodbye!")}");
            Console.WriteLine($"{songLyrics.EndsWith("goodbye")}");
            Console.WriteLine($"{greeting.EndsWith("hello")}");
        }
    }
}