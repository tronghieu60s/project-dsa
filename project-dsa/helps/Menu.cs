using System;
using System.Collections.Generic;
using System.Text;

namespace project_dsa.helps
{
    class Menu
    {
        public int MainMenu()
        {
            int select;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\tMO PHONG ATM\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t*");
            Console.WriteLine("*********************************");
            // select
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ban chon:\t");
            Console.ResetColor();
            int.TryParse(Console.ReadLine(), out select);
            return select;
        }
    }
}
