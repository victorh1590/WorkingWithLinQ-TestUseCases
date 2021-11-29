// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using static System.Console;

// Main body.

// LinqWithArrayOfStrings();
LinqWithArrayOfExceptions();

void LinqWithArrayOfStrings()
{
    var names =  new string[]
    {
        "Michael", "Pam", "Jim", "Dwight", "Angela",
        "Kevin", "Toby", "Creed"
    };
    var query = names
        .Where(name => name.Length > 4)
        .OrderBy(name => name.Length)
        .ThenBy(name => name);
    foreach (string item in query) WriteLine(item);
}

void LinqWithArrayOfExceptions()
{
    var errors = new Exception[]
    {
        new ArgumentException(),
        new SystemException(),
        new IndexOutOfRangeException(),
        new InvalidOperationException(),
        new NullReferenceException(),
        new InvalidCastException(),
        new OverflowException(),
        new DivideByZeroException(),
        new ApplicationException()
    };
    var numberErrors = errors.OfType<ArithmeticException>();
    foreach (var error in numberErrors) WriteLine(error);
}