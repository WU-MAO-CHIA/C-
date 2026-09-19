using System;
using System.Globalization;
// global using System.Text; // global using 可以在整個專案宣告一次就好(GlobalUsings.cs)
using static System.Math;
using CustomerList = System.Collections.Generic.List<MyApp.Models.Customer>;

namespace MyApp.Services; // 當一個檔案只有一種 namespace 就可以不用大括號

class Greeter
{
    public string Greet(string name)
    {
        var culture = CultureInfo.CurrentCulture;
        return $"Hello, {name}! Culture: {culture.Name}";
    }
}

class CircleCalculator
{
    public static double CalculateArea(double radius) => PI * Pow(radius, 2);
    public static double CalculateCircumference(double radius) => 2 * PI * radius;
}

class Customer
{
    public required string Name { get; init; }
    public string? Email { get; init; }

    public override string ToString() => $"{Name} ({Email ?? "no email"})";
}

class CustomerService
{
    public CustomerList GetTopCustomers()
    {
        CustomerList customers = [new() { Name = "Alice" }, new() { Name = "Bob" }];
        return customers;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine(CircleCalculator.CalculateArea(2));
    }

}