namespace MySpace
{
    class Program
    {
        static void WorkWithIntegers()
        {
            int a = 18;
            int b = 6;
            int c = a + b;
            Console.WriteLine(c);


            // subtraction
            c = a - b;
            Console.WriteLine(c);

            // multiplication
            c = a * b;
            Console.WriteLine(c);

            // division
            c = a / b;
            Console.WriteLine(c);        
        }

        static void OrderPrecedence()
        {
            int a = 5;
            int b = 4;
            int c = 2;
            int d = a + b * c;
            Console.WriteLine(d);

            d = (a + b) * c;
            Console.WriteLine(d);

            d = (a + b) - 6 * c + (12 * 4) / 3 + 12;
            Console.WriteLine(d);

            int e = 7;
            int f = 4;
            int g = 3;
            int h = (e + f) / g;
            Console.WriteLine(h);        
        }


        static void Main(string[] args)
        {
            WorkWithIntegers();
            Console.WriteLine();
            OrderPrecedence();
            Console.WriteLine();
            int max = int.MaxValue;
            int min = int.MinValue;
            Console.WriteLine($"The range of integers is {min} to {max}");
            Console.WriteLine($"An example of overflow: {max + 1}");

            double radius = 2.5;
            Console.WriteLine($"The area of curcle: {radius * radius * Math.PI}");
        }
    }
}