/*
null represents the absence of a value. 
When you try to access a member on a null reference, 
by calling a method or reading a property, the runtime throws a NullReferenceException:
*/

using Microsoft.VisualBasic;

string? name = null;

if (name is not null)
{
    Console.WriteLine($"Name has {name.Length} characters");
}
else
{
    Console.WriteLine("Name has no value");
}

/*
C# gives you three complementary tools to write null-safe code:

Nullable value types: let a value type such as int or bool also hold null
Nullable reference types: let the compiler track whether a reference might be null
Null operators: express null-safe access and fallback logic concisely

Nullable value types
Value types such as int, double, and bool can't hold null by default. 
Add ? to the type name to create a nullable value type that holds either a value or null:
*/

int? score = null;
Console.WriteLine(score.HasValue);

score = 95;
Console.WriteLine(score.HasValue);
Console.WriteLine(score.GetValueOrDefault()); // () 裡面放 default 時要return 什麼，預設是 0

int? missing = null;
Console.WriteLine(missing.GetValueOrDefault(-1));

/*
Nullable reference types
Reference types, such as string, arrays, and class instances, can hold null at runtime. Nullable reference types is a compiler feature that makes null intent explicit and catches mistakes at compile time.

By using the ? annotation, you declare your intent:

string? — this reference might be null; the compiler warns if you dereference it without checking first
string — this reference should not be null; the compiler warns if you assign null to it

*/

string? nullableName = null;
string nonnullableName = "Alice";

string display = nullableName?.ToUpper() ?? "(no name)"; //?? 叫 Null-coalescing，左邊有值就用左邊，左邊是 null 才用右邊：
Console.WriteLine(display);

display = nonnullableName.ToUpper();
Console.WriteLine(nonnullableName);


/*
Null operators
C# includes several operators that let you write null-safe code without manual if-null guards everywhere:
*/

string? city = GetCity();

// ?. — access a member only when non-null
int? len = city?.Length;

// ?? — substitute a default when null
string display1 = city ?? "unknown";

if (city is null)
{
    Console.WriteLine("No city provided");
}
else
{
    Console.WriteLine($"{display1} ({len} chars)");
}

string GetCity()
{
    return null;
}

/*
Nullable value types and nullable reference types serve different purposes
Nullable value types and nullable reference types aren't alternatives. They solve different problems:

Use T? for a value type that needs to represent "no value." For example, use int? for an optional database column or DateTime? for an event that isn't scheduled yet.
Use string? and other nullable reference annotations to document that a reference might be null, so the compiler can warn you before a NullReferenceException occurs at runtime.
*/