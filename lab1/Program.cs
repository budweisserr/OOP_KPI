using System;

namespace lab1
{
    public class Program
    {
        static void Main(String[] args)
        {
            var p1 = new GameAccount("First");
            var p2 = new GameAccount("Second");
            p1.WinGame("Second", 40);
            p2.LoseGame("First", 40);
            p1.LoseGame("Second", 25);
            p2.WinGame("First", 25);
            p1.LoseGame("Second", 180);
            p2.WinGame("First", 180);
            Console.WriteLine(p1.GetStats());
            Console.WriteLine(p2.GetStats());
        }
    }
}