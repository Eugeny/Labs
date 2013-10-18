using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pankov.Lab6.Player.UI;

namespace Pankov.Lab6.Player
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Application.Get();
            while (true)
            {
                ConsoleKeyInfo k = Console.ReadKey();
                Application.Get().HandleKey(k);
                if (k.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
            }
        }
    }
}
