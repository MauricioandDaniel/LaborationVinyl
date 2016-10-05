using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylCollection_Console
{
    class ConsoleProperties
    {
        // Mayhaps redundant but useful for displaying how often we use the clear console method
        public static void ClearConsoleWindow()
        {
            Console.Clear();
        }

        public static void Header(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Environment.NewLine + text + Environment.NewLine);
            Console.ResetColor();
        }

        public static void SubHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void RedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WhiteText(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void GreenText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void CyanText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
