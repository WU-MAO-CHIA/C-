using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace MySpace
{
    class Program
    {
        public record struct Point(int X, int Y)
        {
            public double Slope() => (double)Y / (double)X;
        }

        static void DoTuples()
        {
            var pt = (X: 1, Y: 2);
            var slope = (double)pt.Y / (double)pt.X;
            Console.WriteLine($"A line from the origin to the point {pt} has a slope of {slope}.");
            pt.X += 5;
            Console.WriteLine($"The point is now at {pt}.");
            var pt2 = pt with { Y = 10 };
            Console.WriteLine($"The point 'pt2' is at {pt2}.");
            Console.WriteLine();

            var namedData = (Name: "Morning observation", Temp: 17, Wind: 4);
            var person = (FirstName: "", LatName: "");
            var order = (Product: "guitar picks", style: "triangle", quantity: 500, UnitPrice: 0.10m);
            var asdf = (First: namedData, second: person, third: order);
            Console.WriteLine(asdf);
            Console.WriteLine(asdf.First);


            Point pt3 = new Point(1, 1);
            var pt4 = pt3 with { Y = 10 };
            Console.WriteLine($"The two points are {pt3} and {pt4}");

            double slopeResult = pt4.Slope();
            Console.WriteLine($"The slope of {pt4} is {slopeResult}");
        }

        static void Main(string[] args)
        {
            DoTuples();

            int counter = 0;
            do
            {
                Console.WriteLine($"Hello World! The counter is {counter}");
                counter++;
            } while (counter < 10);

            Console.WriteLine();
            counter = 0;
            while (counter < 10)
            {
                Console.WriteLine($"Hello World! The counter is {counter}");
                counter++;
            }

            for (char column = 'a'; column < 'k'; column++)
            {
                Console.WriteLine($"The column is {column}");
            }

            int sum = 0;
            for (int num = 1; num <= 20; num++)
            {
                if (num % 3 == 0)
                {
                    sum += num;
                }
            }
            Console.WriteLine(sum);
        }
    }
}