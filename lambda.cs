/*
Lambda expressions are the C# feature that makes this possible. 
A lambda expression is a compact, inline function that you 
write without giving it a name. Y
ou use the arrow operator => to separate the parameter list 
from the body:

 When there are no parameters or more than one, w
 rap them in parentheses: () => 42 
 or (left, right) => left + right.
*/

/*
A delegate type is a type that represents a method signature. A variable of a delegate type can hold any matching method such as a lambda expression or a named method, as long as its parameter types and return type match.

You declare a delegate type with the delegate keyword:
*/
Transform doubler = x => x * 2;
Transform squarer = Square;

Console.WriteLine(doubler(5));
Console.WriteLine(squarer(5));

Func<int, int, int, double> add = (left, mid, right) => left + mid + right;
Action<string> report = message => Console.WriteLine($"Action:{message}");

Console.WriteLine(add(1, 2, 3));
report($"1 + 2 + 3 = {add(1, 2, 3)}");

int[] numbers = [1, 2, 3, 4, 5, 6];
int[] evennumbers = Filter(numbers, value => value % 2 == 0).ToArray();

Console.WriteLine(string.Join(",", evennumbers));


Func<int, bool> isEven = static value => value % 2 == 0;
Console.WriteLine(isEven(14));
Console.WriteLine(isEven(25));

Action<int, int, string> statusUpdate = (_, _, message) => Console.WriteLine(message);
statusUpdate(200, 42, "Operation completed");

MessagePublisher.MessagePublished += (_, message) => Console.WriteLine($"Received: {message}");
MessagePublisher.MessagePublished += (_, message) => Console.WriteLine($"Subscribe");
// MessagePublisher.Publish("Records updated");

/*
Walking through the code:

MessagePublisher declares event EventHandler<string>? MessagePublished. The event keyword means callers can only subscribe or unsubscribe—they can't invoke it directly.
publisher.MessagePublished += (_, message) => ... subscribes with a lambda expression. The _ discards the sender parameter because this handler doesn't need it.
publisher.Publish("Records updated") raises the event and runs every subscribed handler.
*/

static int Square(int value) => value * value;
static IEnumerable<int> Filter(IEnumerable<int> source, Func<int, bool> predicate) =>
    source.Where(predicate);
delegate int Transform(int value);

static class MessagePublisher
{
    public static event EventHandler<string>? MessagePublished;
    public static void Publish(string message) => MessagePublished?.Invoke(null, message);
}
/*
Built-in delegate types: Func and Action
Func and Action, that cover most scenarios, 
so you rarely need to use the delegate keyword yourself.
System.Func<T,TResult> (and Func<T1, T2, TResult>, and so on) represents a method that returns a value. The last type parameter is always the return type; all earlier ones are input types.
System.Action<T> (and Action<T1, T2>, and so on) represents a method that returns nothing (void). All type parameters are input types. System.Action with no type parameters represents a method with no inputs and no return value.
*/

/*
Pass a lambda expression to a method
When a method declares a Func or Action parameter, 
callers pass a lambda expression that matches that delegate type. 
The compiler checks that the lambda's parameter types and return type match the declared delegate type. 
If they don't match, the code doesn't compile
*/

/*
Keep lambda expressions self-contained
A lambda expression can reference variables from 
the surrounding code. 
Capturing means the lambda holds a reference to a 
variable declared outside its own body. T
he combination of the lambda and the variables it 
captures is called a closure.

When you don't need to capture anything, 
add the static modifier to the lambda. 
A static lambda can only use its own parameters 
and values declared inside its body. 
It can't capture local variables or instance state 
from the enclosing scope.
*/

/*
Use discard parameters when inputs are irrelevant
Sometimes a delegate signature includes parameters you don't need. 
Use the discard _ to signal that choice explicitly.

Common examples include event handlers where you don't 
use sender or EventArgs, callbacks where you only need some 
of several inputs, and LINQ overloads that provide an 
index you don't use.
*/

/*
Events provide optional notifications
An event is a mechanism that one object (the publisher) uses to notify other objects (the subscribers) 
when something happens. 
The publisher doesn't need to know who is listening or how many subscribers there are. 
Subscribers choose to opt in.

Events are built on delegates. 
An event is a delegate field with extra restrictions enforced by 
the event keyword: outside code can only subscribe (+=) 
or unsubscribe (-=) from the event; only the class that declares 
the event can invoke (raise) it

The .NET convention for event delegate types is System.EventHandler<TEventArgs>, 
where T is the type of data included with the notification. 
Its signature always has two parameters: 
the sender (the object that raised the event) and the event data 
of type T.
*/



