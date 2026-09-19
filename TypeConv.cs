/*
A conversion is the process of changing a value from one type to another.
A cast is the explicit syntax for conversion, written with parentheses like (int)value.
An implicit conversion is a conversion that happens automatically when the compiler can guarantee it's safe.
An explicit cast is a conversion you write in code, indicating the conversion might lose information or fail.

Match the conversion style to the situation:

Implicit conversions happen automatically when the compiler can guarantee safety—no syntax required.
Explicit casts are required when data might be lost or the conversion might fail.
Pattern matching or as applies when you need a safe reference-type conversion that might not succeed.
Parsing APIs apply when your source value is text.
*/

//Implicit and explicit numeric conversions
//An implicit conversion always succeeds. An explicit conversion might fail or lose information.

int itemCount = 42;
long widened = itemCount; //implicit conversion

double average = 19.432;
int truncated = (int)average; //explicit cast

Console.WriteLine($"widened: {widened}, truncated: {truncated}");

/*
Convert references
Classes are reference types. Casts on them don't copy data. They change how you view the same object.

Some reference conversions are implicit. 
The compiler guarantees they're safe. 
Three situations always produce an implicit reference conversion: assigning 
a derived class instance to a base class variable 
(the derived type is a subtype of the base type),
assigning a reference type instance to a variable of an interface the type implements, 
and assigning any reference type to an object variable.
*/

Mammal otter = new() { Name = "River otter" };
Animal animal = otter; // Derived to base class: always safe.
INamed named = otter;  // Reference type to implemented interface: always safe.
object obj = otter;   // Any reference type to object: always safe.

Console.WriteLine(named.Name);


/*
Going the other direction, from a base type back to a derived type, 
requires an explicit check, because the object might not actually be the derived type 
you expect. Prefer pattern matching so the test and assignment happen together.
*/

Animal knownAnimal = new Mammal { Name = "aadf" };
if (knownAnimal is Mammal mammal) // compare and assign
{
    Console.WriteLine($"Pattern match succeeded: {mammal.Name}");
}

Animal unknownAnimal = new Reptile();
Console.WriteLine($"Can treat as mammal: {unknownAnimal is Mammal}");

//If you need a nullable result instead of a conditional branch, use as:
object boxedMammal = new Mammal { Name = "Sea lion" };
Mammal? maybeMammal = boxedMammal as Mammal;
Console.WriteLine(maybeMammal is null ? "Not a mammal" : maybeMammal.Name);

object boxedReptile = new Reptile();
Mammal? noMammal = boxedReptile as Mammal;
Console.WriteLine(noMammal is null ? "Safe null result" : noMammal.Name);

/*
Understand boxing and unboxing
Boxing converts a struct or other value type to object or to an implemented interface type. 
Unboxing extracts the value type from that object reference.
*/

int temperature = 72;
object boxedTemperature = temperature; // Boxing
int unboxedTemperature = (int)boxedTemperature; //unboxing

// Packet packet = new(7);
// ILabelled labelledPacket = packet; 
// Console.WriteLine($"Unboxed: {unboxedTemperature}, Label: {labelledPacket.Label}");

/*

Parse text by using Parse and TryParse
When you convert user input or file content, 
start with TryParse. I
t avoids exceptions for expected invalid input and makes 
failure handling explicit. 
All parsing APIs create a new object or value type 
instance from the source string; they don't modify the source.
*/

string textValue = "512";
int parsed = int.Parse(textValue);

string userInput = "12.123";
bool parsedSuccessfully = int.TryParse(userInput, out int safeValue);
Console.WriteLine($"parsed: {parsed}");
Console.WriteLine(parsedSuccessfully ? $"safe value: {safeValue}" : "Input is not a valid number.");


public interface INamed
{
    string Name { get; set; }
}

public class Animal : INamed
{
    public string Name { get; set; }
}

public class Mammal : Animal
{
}

public class Reptile : Animal
{
}

public interface ILabelled
{
    string Label { get; set; }
}

public struct Packet : ILabelled
{
    public string Label { get; set; }
}