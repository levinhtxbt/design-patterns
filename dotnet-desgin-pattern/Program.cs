// See https://aka.ms/new-console-template for more information

Console.WriteLine("Design Patterns in C#");

// Singleton
Console.WriteLine(">> Singleton Pattern <<");
var s1 = DesignPatterns.CreationalPattern.Singleton.GetInstance;
var s2 = DesignPatterns.CreationalPattern.Singleton.GetInstance;

if (s1 == s2)
{
    Console.WriteLine("Both objects are the same instance");
}
else
{
    Console.WriteLine("Both objects are not the same instance");
}

// Singleton with multi-threading
Thread process1 = new Thread(() =>
{
    var s1 = DesignPatterns.CreationalPattern.Singleton.GetInstance("From process1");
    s1.PrintDetails();
});


Thread process2 = new Thread(() =>
{
    var s2 = DesignPatterns.CreationalPattern.Singleton.GetInstance("From process2");
    s2.PrintDetails();
});

process1.Start();
process2.Start();

process1.Join();
process2.Join();

