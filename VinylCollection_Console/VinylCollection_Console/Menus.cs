using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylCollection_Console
{
    class Menus
    {
        private static int startMenuChoice;
        private static int vinylOptionsChoice;

        public static int StartMenuChoice
        {
            get { return startMenuChoice; }
            set { startMenuChoice = value; }
        }

        // Generic menu choice validator
        private static int MenuChoice(int minValue, int maxValue, int arrayEqualizer)
        {
            int choice;
            do
            {
                Console.Write(Environment.NewLine + "Choice: ");
                choice = Int32.Parse(Console.ReadLine()) + arrayEqualizer;
                if (choice < minValue || choice > maxValue)
                    ConsoleProperties.RedText("Invalid input");
            } while (choice < minValue || choice > maxValue);
            return choice;
        }

        public static void StartMenu()
        {
            // Start menu header
            ConsoleProperties.ClearConsoleWindow();
            ConsoleProperties.Header("Your Vinyl Collection");
            ConsoleProperties.SubHeader("   # Artist          Album           Year            Record label\n");
            VinylCollection.DisplayCurrentCollection();

            // Option Text: Exit program
            Console.Write(Environment.NewLine + "Press ");
            ConsoleProperties.WhiteText("  0");
            Console.Write(" to ");
            ConsoleProperties.WhiteText("Exit");
            Console.WriteLine(" program");

            // Option Text: Make changes to vinyl
            Console.Write("Press ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("1-{0}", VinylCollection.RowNumber);
            Console.ResetColor();
            Console.Write(" to ");
            ConsoleProperties.WhiteText("Edit");
            Console.WriteLine(" existing vinyls");

            // Option Text: Add new vinyl
            Console.Write("Press ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("{0,3}", VinylCollection.RowNumber + 1);
            Console.ResetColor();
            Console.Write(" to ");
            ConsoleProperties.WhiteText("Add");
            Console.WriteLine(" a new vinyl");

            startMenuChoice = MenuChoice(-1, VinylCollection.ArtistArray.Length, -1);

            // If statement used rather than switch because of the few cases available
            if (startMenuChoice == -1)
                Console.WriteLine(Environment.NewLine + "Bye for now!");
            else if (startMenuChoice == VinylCollection.ArtistArray.Length)
                VinylCollection.AddNewVinyl();
            else
                VinylOptions();
        }

        private static void VinylOptions()
        {
            // Vinyl options header
            ConsoleProperties.ClearConsoleWindow();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Environment.NewLine + "Currently selected: {0} {1} {2} {3} {4}" + Environment.NewLine,
                startMenuChoice + 1,
                VinylCollection.ArtistArray[startMenuChoice],
                VinylCollection.AlbumArray[startMenuChoice],
                VinylCollection.YearArray[startMenuChoice],
                VinylCollection.RecordLabelArray[startMenuChoice]
                );
            Console.ResetColor();

            // Vinyl options menu
            Console.WriteLine("1. Edit");
            Console.WriteLine("2. Remove");
            Console.WriteLine("3. Return");

            vinylOptionsChoice = MenuChoice(1, 3, 0);

            // If statement used rather than switch because of the few cases available
            if (vinylOptionsChoice == 1)
                VinylCollection.EditVinyl();
            else if (vinylOptionsChoice == 2)
                VinylCollection.RemoveVinyl();
            else
                StartMenu();
        }
    }
}
