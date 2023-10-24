using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        dynamic Locker = new ProtoType();
        Locker.Test = "Hello, World!";
        Locker.Ask = new ProtoType();
        Locker.Ask.To = new ProtoType();
        Locker.Ask.To.A = "1";
        Locker.Ask.To.B = 2;
        Locker.Ask.To.C = ProtoRune<BigInteger>.Fraction("-999999999999999999.99999999999999999");
        Locker.Ask.For = "name";
        Locker.Ask.Question = $"What is your {Locker.Ask.For}?";
        Console.WriteLine(Locker.Ask);
        Console.WriteLine(Locker.Ask.To);
        Console.WriteLine(Locker.Ask.To.A);
        Console.WriteLine(Locker.Ask.To.B);
        Console.WriteLine(Locker.Ask.For);
        Console.WriteLine(Locker.Ask.Question);
        Console.WriteLine(ProtoType.ToJSON(Locker));
        Console.ReadLine();
    }
}