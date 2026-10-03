/*
Nullable reference types are a group of features that minimize the chance your code throws System.NullReferenceException. You declare which variables are intended to hold null and which aren't, and the compiler warns when those declarations don't match how your code uses them. The runtime behavior of your program is unchanged. Nullable reference types are entirely a compile-time feature.

Three building blocks work together:

Variable annotations (string vs. string?) express which references are intended to allow null.
Null-state analysis tracks whether the value of an expression is not-null or maybe-null at each point in your code.
Attributes on APIs describe more nuanced contracts, such as "this argument can be null, but the return value is null only when the argument is null."
The compiler combines these signals to produce diagnostics. Warnings on a non-nullable variable mean the variable might receive null. 
Warnings on a nullable variable mean the code might dereference it without a null check. 
Dereference means to use the value the variable refers to. For example, to call a method on it (variable.Method()), 
read a property (variable.Property), or index into it (variable[0]). 
Dereferencing a variable that has a value of null throws an exception at run time. 
Either kind of warning means the code's behavior doesn't match its stated design.

Express intent with annotations
Every reference type variable is non-nullable by default. Append ? to declare a nullable reference type:
*/

using System.Diagnostics.CodeAnalysis;

DesignIntent();
Annotations();
NullStateTracking();

Node a = new("a") { Parent = null };
Node b = new("b") { Parent = a };
Node c = new("c") { Parent = b };

FlowAnalysis(c);
NullForgiving();

// NullAnalysisAttributes();

DefaultStructPitfall();

static void DesignIntent()
{
    Person p1 = new("Ada", "Lovelace") { MiddleName = "King" };
    Console.WriteLine(p1);

    Person p2 = new("Grace", "Hopper");
    Console.WriteLine(p2);
}


static void Annotations()
{
    string required = "always set"; // non-nullable: assigning null produces a warning
    string? optional = null;        // nullable: holding null is allowed

    Console.WriteLine(required.Length);

    if (optional is not null)
    {
        Console.WriteLine(optional.Length);
    }
}

static void NullStateTracking()
{
    string? message = null;
    // Warning: dereference of a possibly null reference.
    // Console.WriteLine(message.Length);

    message = "Hello world";

    // No warning: the compiler tracks that message is now not-null.
    Console.WriteLine(message.Length);
}

static void FlowAnalysis(Node start)
{
    Node? current = start;
    while (current is not null)
    {
        Console.WriteLine(current.Name);
        current = current.Parent;
    }
}

static void NullForgiving()
{
    string? maybeName = LookUpNmae("ada");
    int length = maybeName!.Length;
    Console.WriteLine(length);
}

static string? LookUpNmae(string id) => id switch
{
    "ada" => "Ada Lovelace",
    _ => null,
};

/*
The NotNullWhenAttribute tells the compiler that when IsPresent returns true, the argument is not-null. 
Inside the if block, the compiler treats value as not-null with no null-forgiving operator required. 
As of .NET 5, all .NET runtime APIs are annotated, so the analysis benefits any code that calls them.
*/

/*
Known pitfalls
Two patterns can leave a non-nullable reference holding null without a warning. 
Both patterns are limitations of the static analysis, not bugs in your code.
*/

/*
Default structs
You can create a struct with non-nullable reference fields by using default or new(). This approach leaves the struct's fields uninitialized:
*/

static void DefaultStructPitfall()
{
    Student s = default;  // No warning, but FirstName and LastName are null.
    Console.WriteLine(s.FirstName?.Length ?? -1);
}

/*
Arrays of references and structs
A new array of a non-nullable reference type contains all null elements until you assign each one:
*/

static void ArrayPitfall()
{
    string[] values = new string[3];      // Elements are null at run time.
    Console.WriteLine(values[0]?.Length ?? -1);

    string[] initialized = ["a", "b", "c"]; // Collection expression initializes every slot.
    Console.WriteLine(initialized[0].Length);
}

/*
The same pitfall applies to arrays of structs: every element starts as the struct's default value, so each element's non-nullable r
eference fields start as null.

Initialize array elements as part of creating the array. Collection expressions (the [1, 2, 3] literal syntax) 
and target-typed new (writing new() when the compiler can infer the type) make full initialization concise.
*/

/*
The fields hold null at run time, but the compiler doesn't warn. 
If you must use a struct, prefer required members, which are members the caller must initialize through an object initializer, 
or a parameterized constructor that callers must invoke.
*/



/*
Use ! sparingly. Each occurrence is a place the compiler can no longer protect you. 
Prefer adding a null check, restructuring the code, or annotating the relevant API so the compiler reaches the right conclusion on its own.
*/

/*
Attributes that describe API contracts
Annotations on a parameter or return type aren't always expressive enough. 
A method might accept a possibly null argument but guarantee a non-null result. 
A test method might return true only when its argument isn't null. Use the nullable analysis attributes to convey these contracts:
*/

// static bool IsPresent([NotNullWhen(true)] string? value) => !string.IsNullOrEmpty(value);

// static void NullAnalysisAttributes()
// {
//     string? input = ReadInput();

//     if (IsPresent(input))
//     {
//         Console.WriteLine($"{input},{input.Length}");
//     }
// }

// static string? ReadInput() => "Hello";

/*
The annotation doesn't change the runtime type. string and string? are both System.String. 
The ? informs the compiler of your design intent. That intent shapes the warnings the compiler produces:

A non-nullable variable has a default null-state of not-null. The compiler warns if you assign a value that might be null.
A nullable variable has a default null-state of maybe-null. The compiler warns if you dereference the variable without first checking it.
Use the annotation to make required and optional values visible in the type system. The following Person type declares 
FirstName and LastName as non-nullable—every person must have both—and MiddleName as nullable, because not everyone has one:
*/

public sealed class Person(string firstName, string lastName)
{
    public string FirstName { get; } = firstName;
    public string? MiddleName { get; init; }
    public string LastName { get; } = lastName;

    public override string ToString() => MiddleName is null ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";
}


/*
Null-state analysis
The compiler tracks the null-state of every expression. The state is one of two values:

not-null: the expression is known to be not null.
maybe-null: the expression might be null.
A local variable's null-state is updated as the compiler analyzes your code. 
Two things change it: assignments and null checks. After an assignment, the variable's null-state matches the expression on the right-hand side. 
If the expression is null or nullable, the variable becomes maybe-null. 
If the expression is a non-null literal, the variable becomes not-null. After a null check, the variable's null-state reflects whichever branch is taken.

In the preceding example, the first dereference produces a warning because message is maybe-null. 
After the assignment to a non-null literal, the compiler knows message is not-null, so the second dereference is safe.

Null-state analysis works across if checks, pattern matching (expressions such as is null or is { } that test the shape of a value), 
and control flow that loops or returns early:
*/

public sealed class Node(string name)
{
    public string Name { get; } = name;
    public Node? Parent { get; init; }
}

/*
Override the warnings with !
Sometimes you know more than the compiler. 
The null-forgiving operator ! declares that an expression is not-null, even when the analysis says otherwise:
*/

public struct Student
{
    public string FirstName;
    public string? MiddleName;
    public string LastName;
}
