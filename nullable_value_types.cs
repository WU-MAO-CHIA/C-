/*
A nullable value type T? represents all values of its underlying value type T, plus an additional null value. A variable of type int? 
holds any integer or null to represent "no value."

Value types such as int, bool, and DateTime can't hold null by default.
This behavior is efficient and prevents many errors. However, this limitation creates a problem when a value might genuinely be absent. 
A common scenario is reading from a database: an integer column might contain a number, or it might contain no value at all (NULL in SQL). 
A plain int can't represent that absence, but int? can.
*/

int? age = null;
double? price = 9.000;
bool? isActive = null;

Console.WriteLine(age.GetValueOrDefault(-1));

age = 39;

int?[] score = [100, null, 85, null, 72];

/*
Check whether a value is present
The recommended way to check a nullable value type and extract its value is with a type pattern:
*/

int? temperature = null;
if (temperature is int degrees) // 把確定不是null 的值給 degress
{
    Console.WriteLine($"Temperature is {degrees}°F.");
}
else
{
    Console.WriteLine("Temperature is not recorded");
}

/*
The is int degrees pattern matches only when temperature is non-null, 
and it simultaneously binds the value to degrees. You get both the null check and the value extraction in one step.
Alternatively, use the HasValue and Value properties:
*/

int? count = 42;

if (count.HasValue)
{
    Console.WriteLine($"Count is {count.Value}.");
}
else
{
    Console.WriteLine("Count has no value.");
}

/*
Prefer the is T value pattern for new code. 
It introduces a new non-nullable variable scoped to the matched branch, 
which makes the intent clearer and eliminates any temptation to accidentally use Value outside a null check, 
where it would throw an InvalidOperationException.

You can also compare directly with null:
*/

int? quantity = null;
if (quantity != null)
{
    Console.WriteLine($"Quantity: {quantity.Value}");
}
else
{
    Console.WriteLine("Quantity is not set.");
}

/*
Get a value with a fallback
When you need a non-nullable value from a nullable, use GetValueOrDefault or the null-coalescing ?? operator:
*/

int? rating = null;

int result1 = rating.GetValueOrDefault();
int result2 = rating.GetValueOrDefault(-1);

// result1 = rating ?? 0;
// result2 = rating ?? -1;

Console.WriteLine(result1);
Console.WriteLine(result2);

rating = 5;
int result3 = rating.GetValueOrDefault(-1);  // 5 (actual value)
Console.WriteLine(result3); // 5

//The ?? operator is often cleaner inline:

int? priority = null;
int effective = priority ?? 0;
Console.WriteLine(effective);

priority = 2;
effective = priority ?? 0;
Console.WriteLine(effective);

//Both approaches return the actual value when one is present, and the fallback you specify when it isn't.

/*
Arithmetic with nullable value types
Arithmetic and comparison operators on nullable value types are lifted: when either operand is null, the result is null rather than an error.
*/

int? a = 10;
int? b = 20;

int? c = null;

int? sum = a + b;
int? sum1 = a + c;

Console.WriteLine($"sum : {sum}"); 
Console.WriteLine($"sum1 : {sum1}, hasvalue: {sum1.HasValue}"); 

/*
Null propagates through arithmetic by default. 
To prevent a null result from cascading further, extract the value with ?? or GetValueOrDefault before you use it in a calculation.
*/