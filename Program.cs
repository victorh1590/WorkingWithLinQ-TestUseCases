// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

// Main body.

// LinqWithArrayOfStrings();
// LinqWithArrayOfExceptions();
LinqWithSets();

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

void Output(IEnumerable<string> cohort, string description = "") 
{
    if (!string.IsNullOrEmpty(description)) WriteLine(description);
    Write(" ");
    WriteLine(string.Join(", ", cohort.ToArray()));
}

void LinqWithSets()
{
    var cohort1 = new string[] { "Rachel", "Gareth", "Jonathan", "George" };
    var cohort2 = new string[] { "Jack", "Stephen", "Daniel", "Jack", "Jared" };
    var cohort3 = new string[] {"Declan", "Jack", "Jack", "Jasmine", "Conor"};
    
    Output(cohort1, "Cohort 1");
    Output(cohort2, "Cohort 2");
    Output(cohort3, "Cohort 3");
    WriteLine();
    
    Output(cohort2.Distinct(), "cohort2.Distinct(): ");
    WriteLine();
    Output(cohort2.Union(cohort3), "cohort2.Union(cohort3): ");
    WriteLine();
    Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3): ");
    WriteLine();
    Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3): ");
    WriteLine();
    Output(cohort2.Except(cohort3), "cohort2.Except(cohort3): ");
    WriteLine();
    Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"), 
        "cohort1.Zip(cohort2) ");
    WriteLine();
}