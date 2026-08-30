namespace MySpace
{
    class Program
    {
        static void DoList()
        {
            List<string> names = ["<name>", "Ann", "Felipe"];
            names.Add("Maria");
            names.Add("Bill");
            names.Remove("Ann");
            foreach (var name in names)
            {
                Console.WriteLine($"Hello {name.ToUpper()}!");
            }
            Console.WriteLine($"My name is {names[0]}.");
            Console.WriteLine($"I've added {names[2]} and {names[3]} to the list.");
            Console.WriteLine($"The list length {names.Count}");
            
            var index = names.IndexOf("Felipe");
            foreach(var name in names)
            {
                Console.WriteLine($"Hello {name.ToUpper()}!");
            }

            if (index == -1)
            {
                Console.WriteLine($"When an item is not found, IndexOf returns {index}");
            }
            else
            {
                Console.WriteLine($"The name {names[index]} is at index {index}");
            }

            index = names.IndexOf("Not Found");
            if (index == -1)
            {
                Console.WriteLine($"When an item is not found, IndexOf returns {index}");
            }
            else
            {
                Console.WriteLine($"The name {names[index]} is at index {index}");
            }

            names.Sort();
            foreach(var name in names)
            {
                Console.WriteLine($"Hello {name.ToUpper()}!");
            }

            List<int> f = [1, 1];

            while(f.Count < 20)
            {
                f.Add(f[f.Count - 1] + f[f.Count - 2]);
            }

            foreach (var i in f)
            {
                Console.WriteLine(i);
            }   
        }

        static void Main(string[] args)
        {
            DoList();
        }

    }
}