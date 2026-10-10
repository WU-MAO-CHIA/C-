/*
When you enable nullable reference types, the compiler issues warnings everywhere your code's behavior doesn't match its annotations. 
Most warnings fall into a small set of patterns. 
Once you recognize the pattern, the fix is usually one of five techniques:
Add a null check.
Add or remove a ? or ! annotation.
Add an attribute that describes the null contract.
Initialize variables correctly.
Verify the project setting.

This article walks through each technique with a representative example. The goal isn't to silence warnings. 
It's to make the code's null-handling intent explicit so the compiler reaches the same conclusions you do.

Null-state: what the compiler tracks
Before you look at the techniques, it helps to know how the compiler tracks potential null-state violations. As it reads your code, the compiler keeps track of each expression's null-state: 
its analysis about whether the expression might be null at that point in the code. The null-state is one of two values:

not-null — the compiler can prove the expression isn't null here. You can safely use it without a check.
maybe-null — the compiler can't rule out null. Using the expression without checking it produces a warning.

A variable's null-state changes as the compiler follows your code. A method that might return null produces a maybe-null result. 
An if (x is not null) check narrows x to not-null inside the if block. 
The warnings you see are the compiler telling you it determined an expression is in a maybe-null state and you're about to use it as 
if it were not-null. Each technique in the rest of this article is a different way to give the compiler the information it 
needs to ensure an expression is not-null before you use it.

Add a null check
The most common warning is possible dereference of null. 
The compiler tracked a variable's null-state to maybe-null and saw the variable used without a check:
*/


// static int LengthOfMessageUnsafe(string? message)
// {
//     // Warning CS8602: dereference of a possibly null reference.
//     return message.Length;
// }

/*
The fix is usually a guard clause. 
A guard clause is a check at the top of a method or block that returns or throws when an input is invalid. 
Only the safe path continues. Once the check runs, the compiler updates the variable's null state to not-null on the safe path:
*/

using System.Diagnostics.CodeAnalysis;

static int DereferenceFixed(string? message)
{
    if (message is null)
    {
        return 0;
    }

    return message.Length;
}

//Pattern matching (expressions such as is null or is { } that test the shape of a value), ??, and ??= include null checks:

static int NUllOperatorsFix(string? message)
{
    // ?. evaluates to null if message is null; ?? supplies the fallback value
    int length = message?.Length ?? 0;
    // Pattern matching narrows the type on the matching branch.
    if (message is {Length: > 0})
    {
        length = message.Length;
    }

    return length;
}

/*
The property pattern { Length: > 0 } matches only when message is non-null 
and its Length property is greater than zero, so the compiler treats message as not-null inside the if block. 
A simpler is not null test produces the same null-state narrowing without inspecting any properties.
*/

/*
Adjust annotations
The compiler also warns you when your code assigns a maybe-null expression to a non-nullable variable. 
That warning means one of two things:

The variable should allow null values. In that case, add a ? to the type.
The expression never produces a null value. Annotate the API that produced it.
*/

static string? LookupName(string id) => id switch
{
    "ada" => "Ada Lovelace",
    _ => null,
};

// static void AssignmentWarning()
// {
//     // Warning CS8600: converting null literal or possible null value to non-nullable type.
//     string name = LookupName("nobody");
//     Console.WriteLine(name);
// }

// If Lookup legitimately returns null, change the call site to accept the missing value:

static void AssignmentFixed()
{
    string? name = LookupName("nobody");
    if (name is not null)
    {
        Console.WriteLine(name);
    }
}

/*
If Lookup never returns null, change its signature to return a non-nullable reference type. 
Scenarios where the null state of the returned value depends on the input, see the following section on null-analysis attributes.

Use the null-forgiving operator ! only when you can guarantee a value isn't null but can't express that guarantee in the type system. 
Each ! is a place the compiler can no longer protect you, so prefer adding a check or annotating the source API.
*/

/*
Add a null-analysis attribute
Sometimes the right fix isn't at the call site. 
A method's signature doesn't capture the relationship between its inputs and outputs precisely enough, 
and the compiler issues warnings inside otherwise-safe code:
*/

static bool IsPresent(string? text) => !string.IsNullOrEmpty(text);

// static void CallerWithOutAttribute(string? text)
// {
//     if (IsPresent(text))
//     {
//          // Warning CS8602: dereference of a possibly null reference.
//          // The signature doesn't tell the compiler text is not-null here.
//         Console.WriteLine(text.Length);
//     }
// }

/*
The body of IsPresent proves the argument isn't null when the method returns true, but the signature doesn't say so. 
Add a nullable analysis attribute to make the contract part of the API:
*/

static bool AttributedIsPresent([NotNullWhen(true)] string? text) => !string.IsNullOrEmpty(text);

static void CallerWithAttribute(string? text)
{
    if (AttributedIsPresent(text))
    {
        // No warning: the attribute tells the compiler text is not-null.
        Console.WriteLine(text.Length);
    }
}

/*
Common attributes include:
NotNullWhenAttribute — the argument is not-null when the method returns the specified Boolean.
NotNullIfNotNullAttribute — the return value is not-null whenever the named argument is not-null.
MemberNotNullAttribute — the listed members are not-null after the method returns.
DoesNotReturnAttribute — the method never returns normally (for example, it always throws).
*/

/*
Initialize non-nullable members
A constructor warning means a non-nullable field, property, 
or auto-property (a property that uses the compiler-generated backing field, 
such as public string Name { get; set; }) exits the constructor without being assigned a non-null value:
*/

// class PersonUninitialized
// {
//     //Warning CS8618: Non-nullable property 'Name' is uninitialized.
//     public string Name {get; set;}
// }

/*
You have several ways to address it. Pick the one that best matches your design intent.
Require the value as a constructor argument. 
Use a primary constructor (parameters declared on the type itself, available throughout the body) 
or a regular constructor that initializes the property:
*/

class PersonInjected(string name)
{
    public string Name {get;} = name;
}

//Make the property required. The caller must initialize it through an object initializer (the { Property = value } syntax that follows new):

class PersonRequired
{
    public required string Name {get; init;}
}

//Initialize with a default. When the type has a meaningful empty value, initialize at the declaration:

class PersonUninitialized
{
    public string Name {get; set;} = "John Doe";
}

//Make the property nullable. When the value really might be missing, change the type to nullable:

class PersonOptional
{
    public string? Name {get; set;}
}

