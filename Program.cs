// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using static System.Console;

LinqWithArrayOfStrings();

void LinqWithArrayOfStrings()
{
    var names =  new string[]
    {
        "Michael", "Pam", "Jim", "Dwight", "Angela",
        "Kevin", "Toby", "Creed"
    };
    var query = names.Where(name => name.Length > 4);
    foreach (string item in query) WriteLine(item);
}