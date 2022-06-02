using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking_Databased
{
    internal class Printings
    {
        // I initially wanted to combine GetTypes and GetOffice but they were different kind of lists.
        // Combining them made it more confusing than having some repetition by using two methods.
        // Therefore, I have two methods that do relatively the same but for different list types.
        public static int GetTypes(DemoDBContext context, int edit = -1)
        {
            List<ProductType> results = context.ProductTypes.ToList();
            int i = 1;

            // Clearing console after loading the database properly
            Console.Clear();

            // If edit is NOT -1, it means we're editting an item. Adding a warning for this.
            if (edit != -1)
            {
                Products editProduct = context.Products.SingleOrDefault(x => x.ID == edit);
                ErrorMessage("WARNING: You are editting a product: " + editProduct.Brand + " " + editProduct.Model + "\n");
            }

            Console.WriteLine("Below is a list of the different product types.");
            foreach (var type in results)
            {
                Console.WriteLine($"{i}) {type.TypeName}");
                i++;
            }
            Console.Write("\n\nPlease enter the number of the product type you'd like to add a product to: ");
            while (true)
            {
                string answer = Console.ReadLine();
                try
                {
                    int.TryParse(answer, out int result);
                    SuccessMessage($"You are adding an item to: {results[result - 1].TypeName}\n");
                    return Convert.ToInt32(answer);
                }
                // This catch will work whether it's not a number, or the number is too big / small
                catch
                {
                    ErrorMessage("Invalid answer, please try again.\nEnter the product type number: ");
                }
            }
        }

        public static int GetOffice(DemoDBContext context)
        {
            List<Office> results = context.Offices.ToList();
            int i = 1;

            // Offices doesn't need a clear as the database has already been loaded properly
            Console.WriteLine("Below is a list of the different offices.");
            foreach (var office in results)
            {
                Console.WriteLine($"{i}) {office.OfficePlace}");
                i++;
            }
            Console.Write("\n\nPlease enter the number of the office you'd like to add a product to: ");
            while (true)
            {
                string answer = Console.ReadLine();
                try
                {
                    int.TryParse(answer, out int result);
                    SuccessMessage($"You are adding an item to: {results[result - 1].OfficePlace}\n");
                    return Convert.ToInt32(answer);
                }
                // This catch will work whether it's not a number, or the number is too big / small
                catch
                {
                    ErrorMessage("Invalid answer, please try again.\nEnter the product office number: ");
                }
            }
        }

        public static ConsoleKeyInfo MainMenu()
        {
            // Options within the main menu, and other variables
            string[] options = new string[]
            {
                "C: Add an item",
                "R: Show all items",
                "U: Edit an item",
                "D: Delete an item"
            };
            ConsoleKeyInfo cKey;

            // Printing options, now with colour
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Below are your options.");
            Console.ResetColor();
            foreach (string option in options)
            {
                Console.WriteLine(option);
            }
            Console.Write("\nPress 'escape' to close the application.\nPlease enter the desired letter; C, R, U, or D: ");

            // Getting choice. Will reset if not CRUD. Escape to exit environment.
            while (true)
            {
                cKey = Console.ReadKey(true);
                switch (cKey.Key)
                {
                    // Right answers return the result
                    case ConsoleKey.C or ConsoleKey.R or ConsoleKey.U or ConsoleKey.D:
                        return cKey;

                    // God knows why this case still wants a "break;" even though it literally closes the application. Like, seriously.
                    case ConsoleKey.Escape:
                        Environment.Exit(420);
                        break;

                    // Wrong answers only
                    default:
                        break;
                }
            }

        }

        public static void ErrorMessage(string errorName)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(errorName);
            Console.ResetColor();
        }

        public static void SuccessMessage(string successName)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(successName);
            Console.ResetColor();
        }

        public static void ReadingTitles()
        {
            Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}",
               "Type".PadRight(15),
               "Brand".PadRight(15),
               "Model".PadRight(15),
               "Office".PadRight(15),
               "Purchase Date".PadRight(15),
               "Price in GBP".PadRight(15),
               "Currency".PadRight(10),
               "Local Price Today");

            Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}",
                "----".PadRight(15),
                "-----".PadRight(15),
                "-----".PadRight(15),
                "------".PadRight(15),
                "-------------".PadRight(15),
                "-----------".PadRight(15),
                "--------".PadRight(10),
                "-----------------");
        }

        public static string MakeSmaller(string input)
        {
            return input.Length <= 15 ? input : input.Substring(0, 12) + "...";
        }
    }
}