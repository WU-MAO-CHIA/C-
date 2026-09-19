/*
Tuples and deconstruction

A tuple groups multiple values into a single, lightweight structure without requiring you to define a named type. 
Tuples are value types that you can declare inline, return from methods, 
and deconstruct into individual variables. 
Use tuples when you need a quick, temporary grouping of related values. 
For example, when you return multiple results from a method or store a 
coordinate pair.

uples work well for short-lived groupings where defining a class, struct, 
or record would add unnecessary ceremony. For long-lived domain concepts or 
types with behavior, prefer records, classes, or structs. 
For a comparison of when to use each, see Choose which kind of type.
*/

using System.Drawing;

var location = (Latitude: 234.23424, Longtiude: -123123.12312);
Console.WriteLine($"Location: {location.Latitude}, {location.Longtiude}");

//Declare and initialize tuples
// Tuple with named elements
(string name, int age) person = ("aaa", 20);
Console.WriteLine($"{person.name} is {person.age} years old");

// Tuple with default element names (Item1, Item2)
(string, int) unnamed = ("boob", 90);
Console.WriteLine($"{unnamed.Item1} is {unnamed.Item2} years old");

// Tuple declared with var and inline names
var city = (Name: "Seattle", Population: 749256);
Console.WriteLine($"{city.Name}: population {city.Population}");

var name = "Carol";
var age = 28;

// The compiler infers element names from the variable names
var person1 = (name, age);
Console.WriteLine($"{person1.name} is {person1.age}");
// Output: Carol is 28

/*
Return multiple values from a method
One of the most common uses for tuples is returning multiple values from a method. 
Instead of defining a class or using out parameters, return a tuple with named elements:
*/

List<double> test = [1d, 2d, 3d];
var rr = ComputeStats(test);
Console.WriteLine($"Min: {rr.Minimum}, Max: {rr.Maximum}, Average: {rr.Average}");

static (double Minimum, double Maximum, double Average) ComputeStats(List<double> values)
{
    var min = values.Min();
    var max = values.Max();
    var avg = values.Average();
    return (min, max, avg);
}

/*
Deconstruct tuples
Deconstruction unpacks a tuple's elements into separate variables in a single statement. 
You can deconstruct tuples in several ways:
*/

List<(string Name, int Score)> results =
[
    ("Alice", 92),
    ("Bob", 87),
    ("Carol", 95)
];

foreach (var (n, score) in results)
{
    Console.WriteLine($"{n}: {score}");
}

// When you don't need every element, use a discard (_) in place of each value you want 
// to ignore. Use a separate _ for each discarded position:

List<double> values = [5.0, 2.3123, 123.32];
var (_, max, _) = ComputeStats(values);
Console.WriteLine($"Only need the max: {max}");

/*
Tuple equality
You can compare tuples using == and !=. These operators compare each element in order, 
so two tuples are equal when all their corresponding elements are equal:
*/

var order1 = (Product: "Widget", Quantity: 5);
var order2 = (Product: "Widget", Quantity: 5);
var order3 = (Product: "Gadget", Quantity: 3);

Console.WriteLine(order1 == order2); // True
Console.WriteLine(order1 == order3); // False

// Element names don't affect equality—only values matter
var named = (X: 1, Y: 2);
var different = (A: 1, B: 2);
Console.WriteLine(named == different); // True

/*
Nondestructive mutation with with
The with expression creates a copy of a tuple with one or more elements changed,
leaving the original unchanged:

This pattern is useful when you want a variation of an existing tuple without modifying the original. 
The with expression works the same way on tuples as it does on records.
*/

var original1 = (Name: "Widget", Price: 20.33m, InStock: true);
var discounted = original1 with { Price = 2.33m };
Console.WriteLine($"Original: {original1.Name} at {original1.Price:C}");
Console.WriteLine($"Discounted: {discounted.Name} at {discounted.Price:C}");

/*
Tuples in dictionaries and lookups
Tuples make convenient dictionary values when you need to associate a key with 
multiple pieces of data:
*/

var sizeChart = new Dictionary<string, (int Min, int Max)>
{
    ["Small"] = (0, 50),
    ["Medium"] = (51, 100),
    ["Large"] = (101, 200)
};

if (sizeChart.TryGetValue("Medium", out var range))
{
    Console.WriteLine($"Medium: {range.Min}–{range.Max}");
}

/*
Tuples also work as dictionary keys, giving you a composite key without defining a custom type.
Because tuples implement structural equality, lookups match on the combined values 
of all elements:
*/

var grid = new Dictionary<(int row, int column), string>
{
    [(0, 0)] = "Origin",
    [(1, 3)] = "Sensor A",
    [(2, 5)] = "Sensor B"
};

var target = (row: 1, column: 3);
if (grid.TryGetValue(target, out var label))
{
    Console.WriteLine($"({target.row}, {target.column}): {label}");
}