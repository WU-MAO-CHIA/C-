/*
Anonymous types (C# Programming Guide)
Anonymous types provide a convenient way to encapsulate a set of read-only 
properties into a single object without defining a named type first. 
The compiler generates a type name at compile time that you can't 
access in your source code. 
The compiler infers the type of each property.

Create anonymous types by using the new operator together with an 
object initializer. The following example shows an anonymous type 
that's initialized with two properties, Name and Age:
*/

var person = new { Name = "Alice", Age = 30 };
Console.WriteLine($"{person.Name} is {person.Age} years old.");

/*
Inferred property names
You can specify property names explicitly by using the Name = value 
syntax. When you initialize an anonymous type with a variable or 
member access expression, the compiler infers the property name 
from that expression:
*/

string productName = "Laptop";
decimal price = 999.99m;
var product = new { productName, price };
Console.WriteLine($"{product.productName}: {product.price:C}");

/*
Declare anonymous types with var
Because the compiler generates the type name and you can't access 
it in source code, you must use var to declare the local variable. 
You can't specify the type name explicitly:
*/

/*
Use anonymous types in LINQ queries
Anonymous types appear most often in the select clause of a query expression, 
where they project a subset of properties from each source element:
*/

var words = new[] { "apple", "blueberry", "cherry" };
var results = words.Select(w => new { Word = w, Length = w.Length });

foreach (var item in results)
{
    Console.WriteLine($"{item.Word} has {item.Length} letters.");
}

/*
Equality
Two anonymous type instances that have the same property names and 
types in the same order share the same compiler-generated type. 
The compiler overrides Equals and GetHashCode so that equality 
compares property values rather than reference identity:
*/

var a = new { Name = "Alice", Age = 30 };
var b = new { Name = "Alice", Age = 30 };
var c = new { Name = "Bob", Age = 25 };

Console.WriteLine(a.Equals(b));
Console.WriteLine(a.Equals(c));

/*
Nested anonymous types
Anonymous types can contain other anonymous types as property values:
*/

var order = new
{
    OrderId = 1,
    Customer = new { Name = "Alice", City = "Seattle" },
    Total = 150.00m
};

Console.WriteLine($"Order {order.OrderId} for {order.Customer.Name} in {order.Customer.City}");

order.OrderId = 2;
Console.WriteLine($"Order {order.OrderId} for {order.Customer.Name} in {order.Customer.City}");

/*
Characteristics
Anonymous types have the following characteristics:

The compiler generates them as internal sealed class types that derive from Object.
All properties are public and read-only.
Anonymous types support with expressions for nondestructive mutation.
The compiler generates value-based Equals, GetHashCode, and ToString overrides.
Anonymous types support expression trees, while tuples don't.


Limitations
Anonymous types have several limitations:

You can't use them as method return types, method parameters, or field types because you can't name the type.
They're scoped to the method where you declare them.
You can't add methods, events, or custom operators.
Properties are always read-only; anonymous types don't support mutable properties.
*/