// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using Microsoft.EntityFrameworkCore;
using WorkingWithLinQ_TestUseCases;

// Main body.

// LinqWithArrayOfStrings();
// LinqWithArrayOfExceptions();
// LinqWithSets();
// FilterAndSort();
// JoinCategoriesAndProducts();
// GroupJoinCategoriesAndProducts();
AggregateProducts();

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

void FilterAndSort()
{
    using var db = new Northwind();
    var query = db.Products
        .Where(product => product.UnitPrice < 10M)
        .OrderByDescending(product => product.UnitPrice)
        .Select(product => new
        {
            product.ProductID,
            product.ProductName,
            product.UnitPrice
        });
    
    WriteLine("Products that cost less than $10: ");
    foreach (var item in query)
    {
        WriteLine("{0}: {1} costs {2:$#,##0.00}", 
            item.ProductID, item.ProductName, item.UnitPrice);
    }
    WriteLine();
}

void JoinCategoriesAndProducts()
{
    using var db = new Northwind();
    var queryJoin = db.Categories.Join(
        inner: db.Products,
        outerKeySelector: category => category.CategoryID,
        innerKeySelector: product => product.CategoryID,
        resultSelector: (c, p) => 
            new { c.CategoryName, p.ProductName, p.ProductID }
        )
        .OrderBy(cp => cp.CategoryName);

    foreach (var item in queryJoin)
    {
        WriteLine("{0}: {1} is in {2}.",
            arg0: item.ProductID,
            arg1: item.ProductName,
            arg2: item.CategoryName);
    }
}

void GroupJoinCategoriesAndProducts()
{
    using var db = new Northwind();
    var queryGroup = db.Categories
        .AsEnumerable()
        .GroupJoin(
        inner: db.Products,
        outerKeySelector: category => category.CategoryID,
        innerKeySelector: product => product.CategoryID,
        resultSelector: (category, matchingProducts) => new
        {
            category.CategoryName,
            Products = matchingProducts.OrderBy(p => p.ProductName)
        });

    foreach (var item in queryGroup)
    {
        WriteLine("{0} has {1} products.",
            arg0: item.CategoryName,
            arg1: item.Products.Count());

        foreach (var product in item.Products) WriteLine($" {product.ProductName}");
    }
}

void AggregateProducts()
{
    using var db = new Northwind();
    WriteLine("{0,-25} {1,10}",
        arg0: "Product count:",
        arg1: db.Products.Count());
    WriteLine("{0,-25} {1,10:$#,##0.00}",
        arg0: "Highest product price: ",
        arg1: db.Products.Max(p => p.UnitPrice));
    WriteLine("{0,-25} {1,10:N0}",
        arg0: "Sum of units in stock: ",
        arg1: db.Products.Sum(p => p.UnitsInStock));
    WriteLine("{0,-25} {1,10:N0}",
        arg0: "Sum of units in order: ",
        arg1: db.Products.Sum(p => p.UnitsOnOrder));
    WriteLine("{0,-25} {1,10:$#,##0.00}",
        arg0: "Average unit price: ",
        arg1: db.Products.Average(p => p.UnitPrice));
    WriteLine("{0,-25} {1,10:$#,##0.00}",
        arg0: "Value of units in stock: ",
        arg1: db.Products.AsEnumerable().Sum(p => p.UnitPrice * p. UnitsInStock));
}