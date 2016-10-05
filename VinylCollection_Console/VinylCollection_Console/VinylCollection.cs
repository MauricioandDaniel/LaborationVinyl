using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VinylCollection_Console
{
    class VinylCollection
    {

        #region Variables
        // Artist | Album | Year | Record label (4 parameters)
        private const int NUM_OF_PARAMETERS = 4;

        private static string[] currentVinylCollection;
        private static string[] splitArray;
        private static int rowNumber;
        private static int numOfRows;

        // Vinyl collection data
        private static string[] artistArray;
        private static string[] albumArray;
        private static int[] yearArray;
        private static string[] recordLabelArray;

        // Edit existing vinyl
        private static string editArtist;
        private static string editAlbum;
        private static int editYear;
        private static string editYearString;
        private static string editRecordLabel;

        // New vinyl data
        private static string newArtist;
        private static string newAlbum;
        private static int newYear;
        private static string newRecordLabel;

        // Temporary variables for use when removing or adding vinyls
        private static string[] tempArtistArray;
        private static string[] tempAlbumArray;
        private static int[] tempYearArray;
        private static string[] tempRecordLabelArray;
        #endregion

        #region Properties
        public static string[] ArtistArray
        {
            get { return artistArray; }
            set { artistArray = value; }
        }

        public static string[] AlbumArray
        {
            get { return albumArray; }
            set { albumArray = value; }
        }

        public static int[] YearArray
        {
            get { return yearArray; }
            set { yearArray = value; }
        }

        public static string[] RecordLabelArray
        {
            get { return recordLabelArray; }
            set { recordLabelArray = value; }
        }

        public static int RowNumber
        {
            get { return rowNumber; }
            set { rowNumber = value; }
        }
        #endregion

        public static void DisplayCurrentCollection()
        {
            ResetVariables();
            RetrieveListData();
            PrintCollection();
        }

        private static void ResetVariables()
        {
            rowNumber = 0;
            splitArray = new string[NUM_OF_PARAMETERS];
        }

        private static void RetrieveListData()
        {
            currentVinylCollection = File.ReadAllLines(@"VinylCollection.txt");
            numOfRows = currentVinylCollection.Length;
            artistArray = new string[currentVinylCollection.Length];
            albumArray = new string[currentVinylCollection.Length];
            yearArray = new int[currentVinylCollection.Length];
            recordLabelArray = new string[currentVinylCollection.Length];

            for (int i = 0; i < currentVinylCollection.Length; i++)
            {
                splitArray = currentVinylCollection[i].Split('.');
                artistArray[i] = splitArray[0];
                albumArray[i] = splitArray[1];
                yearArray[i] = Int32.Parse(splitArray[2]);
                recordLabelArray[i] = splitArray[3];
            }
        }

        private static void PrintCollection()
        {
            for (int i = 0; i < artistArray.Length; i++)
            {
                rowNumber++;
                Console.Write("{0,4} {1,-15} {2,-15} {3,-15} {4}" + Environment.NewLine,
                    rowNumber, artistArray[i], albumArray[i], yearArray[i], recordLabelArray[i]);
            }
        }

        public static void UpdateData()
        {
            for (int i = 0; i < numOfRows; i++)
            {
                currentVinylCollection[i] = ArtistArray[i] + "." + AlbumArray[i] + "."
                    + YearArray[i] + "." + RecordLabelArray[i];
            }
            File.WriteAllLines(@"VinylCollection.txt", currentVinylCollection);
        }

        #region Edit Vinyl
        public static void EditVinyl()
        {

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Press Enter to leave value unchanged.");
            Console.WriteLine("-------------------------------------");

            // Edit name
            ConsoleProperties.CyanText("Artist" + Environment.NewLine);
            Console.WriteLine("Old: {0}", ArtistArray[Menus.StartMenuChoice]);
            ConsoleProperties.WhiteText("New: ");
            editArtist = Console.ReadLine();
            if (editArtist == String.Empty)
                ConsoleProperties.GreenText("No changes made" + Environment.NewLine);
            else
            {
                ArtistArray[Menus.StartMenuChoice] = editArtist;
                ConsoleProperties.GreenText("Changes set!" + Environment.NewLine);
            }

            // Edit album
            ConsoleProperties.CyanText("Album" + Environment.NewLine);
            Console.WriteLine("Old: {0}", AlbumArray[Menus.StartMenuChoice]);
            ConsoleProperties.WhiteText("New: ");
            editAlbum = Console.ReadLine();
            if (editAlbum == String.Empty)
                ConsoleProperties.GreenText("No changes made" + Environment.NewLine);
            else
            {
                AlbumArray[Menus.StartMenuChoice] = editAlbum;
                ConsoleProperties.GreenText("Changes set!" + Environment.NewLine);
            }

            // Edit Year
            ConsoleProperties.CyanText("Year" + Environment.NewLine);
            Console.WriteLine("Old: {0}", YearArray[Menus.StartMenuChoice]);
            ConsoleProperties.WhiteText("New: ");
            editYearString = Console.ReadLine();
            if (editYearString == String.Empty)
                ConsoleProperties.GreenText("No changes made" + Environment.NewLine);
            else
            {
                editYear = Int32.Parse(editYearString);
                YearArray[Menus.StartMenuChoice] = editYear;
                ConsoleProperties.GreenText("Changes set!" + Environment.NewLine);
            }

            // Edit record label
            ConsoleProperties.CyanText("Record label" + Environment.NewLine);
            Console.WriteLine("Old: {0}", RecordLabelArray[Menus.StartMenuChoice]);
            ConsoleProperties.WhiteText("New: ");
            editRecordLabel = Console.ReadLine();
            if (editRecordLabel == String.Empty)
                ConsoleProperties.GreenText("No changes made" + Environment.NewLine);
            else
            {
                RecordLabelArray[Menus.StartMenuChoice] = editRecordLabel;
                ConsoleProperties.GreenText("Changes set!" + Environment.NewLine);
            }

            UpdateData();

            ConsoleProperties.ClearConsoleWindow();
            ConsoleProperties.Header("All set!");      
            Console.WriteLine("Revised vinyl: \n");
            Console.WriteLine("\t" + ArtistArray[Menus.StartMenuChoice] + " " + AlbumArray[Menus.StartMenuChoice] + " "
                + YearArray[Menus.StartMenuChoice] + " " + RecordLabelArray[Menus.StartMenuChoice]);
            Console.WriteLine(Environment.NewLine + "Press enter to return to Start Menu");
            Console.ReadKey();
            Menus.StartMenu();
        }
        #endregion

        #region Remove Vinyl
        public static void RemoveVinyl()
        {

            tempArtistArray = new string[numOfRows - 1];
            tempAlbumArray = new string[numOfRows - 1];
            tempYearArray = new int[numOfRows - 1];
            tempRecordLabelArray = new string[numOfRows - 1];

            for (int i = 0; i < numOfRows; i++)
            {
                if (Menus.StartMenuChoice == i)
                {
                    for (int j = i + 1; j < numOfRows; j++)
                    {
                        tempArtistArray[j - 1] = ArtistArray[j];
                        tempAlbumArray[j - 1] = AlbumArray[j];
                        tempYearArray[j - 1] = YearArray[j];
                        tempRecordLabelArray[j - 1] = RecordLabelArray[j];
                    }
                    break;
                }
                else
                {
                    tempArtistArray[i] = ArtistArray[i];
                    tempAlbumArray[i] = AlbumArray[i];
                    tempYearArray[i] = YearArray[i];
                    tempRecordLabelArray[i] = RecordLabelArray[i];
                }
            }

            numOfRows--;
            currentVinylCollection = new string[numOfRows];

            ConsoleProperties.RedText("\nCertain? Enter Y/y to confirm ");
            Console.Write("(any other key cancels the current operation): ");
            string deleteQuery = Console.ReadLine();

            if (deleteQuery == "Y" || deleteQuery == "y")
            {
                ArtistArray = new string[numOfRows];
                AlbumArray = new string[numOfRows];
                YearArray = new int[numOfRows];
                RecordLabelArray = new string[numOfRows];

                for (int i = 0; i < numOfRows; i++)
                {
                    ArtistArray[i] = tempArtistArray[i];
                    AlbumArray[i] = tempAlbumArray[i];
                    YearArray[i] = tempYearArray[i];
                    RecordLabelArray[i] = tempRecordLabelArray[i];
                }

                UpdateData();

                ConsoleProperties.ClearConsoleWindow();
                ConsoleProperties.Header("Vinyl deleted");
                Console.WriteLine("Press enter to return to Start Menu");
                Console.ReadKey();
                Menus.StartMenu();
            }
            else
            {
                ConsoleProperties.ClearConsoleWindow();
                ConsoleProperties.Header("No changes made");
                Console.WriteLine("Press enter to return to Start Menu");
                Console.ReadKey();
                Menus.StartMenu();
            }
        }
        #endregion

        #region Add Vinyl
        public static void AddNewVinyl()
        {
            ConsoleProperties.ClearConsoleWindow();
            ConsoleProperties.Header("New Vinyl");

            tempArtistArray = new string[numOfRows];
            tempAlbumArray = new string[numOfRows];
            tempYearArray = new int[numOfRows];
            tempRecordLabelArray = new string[numOfRows];
            numOfRows += 1;

            for (int i = 0; i < currentVinylCollection.Length; i++)
            {
                tempArtistArray[i] = ArtistArray[i];
                tempAlbumArray[i] = AlbumArray[i];
                tempYearArray[i] = YearArray[i];
                tempRecordLabelArray[i] = RecordLabelArray[i];
            }

            currentVinylCollection = new string[numOfRows];

            ArtistArray = new string[numOfRows];
            AlbumArray = new string[numOfRows];
            YearArray = new int[numOfRows];
            RecordLabelArray = new string[numOfRows];

            // Edit name
            Console.Write("Artist: ");
            newArtist = Console.ReadLine();

            // Edit album
            Console.Write("Album: ");
            newAlbum = Console.ReadLine();

            // Edit Year
            Console.Write("Year: ");
            newYear = Int32.Parse(Console.ReadLine());

            // Edit record label
            Console.Write("Record label: ");
            newRecordLabel = Console.ReadLine();

            for (int i = 0; i < numOfRows; i++)
            {
                if (i == numOfRows - 1)
                {
                    ArtistArray[i] = newArtist;
                    AlbumArray[i] = newAlbum;
                    YearArray[i] = newYear;
                    RecordLabelArray[i] = newRecordLabel;
                }
                else
                {
                    ArtistArray[i] = tempArtistArray[i];
                    AlbumArray[i] = tempAlbumArray[i];
                    YearArray[i] = tempYearArray[i];
                    RecordLabelArray[i] = tempRecordLabelArray[i];
                }
            }

            UpdateData();
            ConsoleProperties.ClearConsoleWindow();
            ConsoleProperties.Header("All set!");
            Console.WriteLine("Your latest vinyl is: \n");
            Console.WriteLine("\t" + ArtistArray[numOfRows - 1] + " " + AlbumArray[numOfRows - 1] + " "
                + YearArray[numOfRows - 1] + " " + RecordLabelArray[numOfRows - 1]);
            Console.WriteLine("\nPress enter to return to Start Menu");
            Console.ReadKey();
            Menus.StartMenu();
        }
        #endregion
    }
}
