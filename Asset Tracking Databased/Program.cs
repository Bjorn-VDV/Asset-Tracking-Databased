using Asset_Tracking_Databased;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Migrations;


Printings.SuccessMessage("\nPlease wait as the database gets created.");
DemoDBContext context = new DemoDBContext();
await context.Database.MigrateAsync();


Console.ResetColor();
Start();

// var test1 = context.Products.Include(x => x.Office).Include(x => x.ProductType).ToList();

// I could use a while loop, but restarting the code by going directly to the start when
// calling the method creates less potential for code breaking somewhere by accident with
// overwriting things



static void Start()
{
    // Starting variables + unicode creation. Clearing in case of recalling start
    DemoDBContext context = new DemoDBContext();
    Console.OutputEncoding = System.Text.Encoding.Unicode;
    Console.Clear();

    // Running main menu, getting result choice back
    ConsoleKeyInfo c = Printings.MainMenu();
    Printings.SuccessMessage("\nPlease wait as the database loads.");

    switch (c.Key)
    {
        case ConsoleKey.C:
            ProductChanges(context);
            break;

        case ConsoleKey.R:
            ReadingProducts(context);
            break;

        case ConsoleKey.U:
            int toEdit = ReadingProducts(context, true);
            ProductChanges(context, toEdit);
            break;

        case ConsoleKey.D:
            int toDel = ReadingProducts(context, true);
            DeleteItem(context, toDel);
            break;
    }

}

static void ProductChanges(DemoDBContext context, int edit = -1)
{
    // while loop for repeated adding
    while (true)
    {
        int whichType, officeNr;
        float price;
        string? brand, model, answer;
        DateTime date;
        ConsoleKeyInfo key;

        // asking what to add. Using empty CW to add a space before the prompts
        Console.WriteLine("");

        whichType = Printings.GetTypes(context, edit) - 1;

        // Obtaining item brand and model
        Console.Write("Please provide the item's brand: ");
        brand = Console.ReadLine();
        Console.Write("Please provide the item's model: ");
        model = Console.ReadLine();

        // Obtaining the item's Office. Using empty CW to add a space before the prompts
        Console.WriteLine("");
        // Removing 1 (for now) to print the proper listnumber later for confirmation
        officeNr = Printings.GetOffice(context) - 1;

        // Obtaining the price
        while (true)
        {
            Console.Write("Please provide the item's price (in GBP): ");
            try
            {
                price = Convert.ToInt32(Convert.ToDouble(Console.ReadLine()) * 100);
                break;
            }
            catch
            {
                Printings.ErrorMessage("\nThis is not a valid number, please try again.");
            }
        }

        // Getting the date
        while (true)
        {
            Console.WriteLine("Is this item to be added today, or from a previous point in time? 1/2");
            answer = Console.ReadLine();
            if (answer == "1")
            {
                date = DateTime.Now;
                break;
            }
            else if (answer == "2")
            {
            DateRetry:
                Console.Write("Please input a date in the \"yyyy-MM-dd\" format: ");
                // Trying to make it a date to avoid any potential future issues in conversions
                try
                {
                    date = Convert.ToDateTime(Console.ReadLine());
                    break;
                }
                catch
                {
                    Printings.ErrorMessage("That was not a valid date. Please try again.\n"); goto DateRetry;
                }
            }
            else { Printings.ErrorMessage("Please answer with 1 or 2."); }
        }

        // Setting variables to prompt additions
        Console.Clear();
        List<ProductType> productTypes = context.ProductTypes.ToList();
        List<Office> offices = context.Offices.ToList();

        // This line sums up what we're adding so that the reader can see whether it's correct or not
        Console.WriteLine($"Adding a product to {productTypes[whichType].TypeName}." +
            $"\nIt is a: '{brand}', model '{model}'. Office '{offices[officeNr].OfficePlace}'. Priced at £ {price / 100}." +
            $"\nItem to be added on {date}" +
            $"\n\nIs this correct? Y/N");
        while (true)
        {
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Y)
            {
                // We are adding +1 again to officeNr and whichType so it matches with the database numbers
                // If the edit parameter is -1, it means we didn't mean to change anything, and we just add
                // Otherwise, we change an item in the database using the new values
                if (edit == -1)
                {
                    context.Products.Add(new Products { OfficeID = officeNr + 1, ProductTypeID = whichType + 1, Brand = brand, Model = model, Date = date.ToString("yyyy-MM-dd"), UKPrice = price / 100 });
                }
                else
                {
                    Products toEdit = context.Products.FirstOrDefault(x => x.ID == edit);
                    toEdit.OfficeID = officeNr + 1;
                    toEdit.ProductTypeID = whichType + 1;
                    toEdit.Brand = brand;
                    toEdit.Model = model;
                    toEdit.Date = date.ToString("yyyy-MM-dd");
                    toEdit.UKPrice = (price / 100);
                }
                context.SaveChanges();
                break;
            }

            // Mind changed: Made a mistake or prefer not adding. Using Sleep to make the prompt readable before we clear the console.
            else if (key.Key == ConsoleKey.N)
            {
                Console.WriteLine("Item not added.\n");
                Thread.Sleep(1000);
                break;
            }
        }

        // Asking to add more or not. Doing this at the start for testing purposes.
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Add another item? Y/N");
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Y)
            {
                Printings.SuccessMessage("Please wait as the database reloads.\n");
                break;
            }
            else if (key.Key == ConsoleKey.N)
            {
                Start();
            }
        }
    }
}

static int ReadingProducts(DemoDBContext context, bool scroll = false)
{
    // Fetching the entire list including information about offices and product type
    List<Products> products = context.Products.Include(x => x.ProductType).Include(x => x.Office).ToList();
    Console.Clear();
    int choice = 0;

    // Sorting the list directly
    products = products.OrderBy(x => x.ProductTypeID).ThenBy(x => x.Date).ToList();

    // Printing the list
    do
    {
        // Resetting listpos here for colouring
        int listPos = 0;

        // Clearing, printing title names
        Console.Clear();
        Printings.ReadingTitles();

        // Item printings
        foreach (Products product in products)
        {
            // Checking if the item matches with your personal choice when scrolling. If so, colours!
            if (scroll == true && choice == listPos)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }

            // Printing the actual stuff
            string prodModShort = Printings.MakeSmaller(product.Model);
            Console.WriteLine(
                        product.ProductType.TypeName.PadRight(15) +
                        product.Brand.PadRight(15) +
                        prodModShort.PadRight(15) +
                        product.Office.OfficePlace.PadRight(15) +
                        product.Date.PadRight(15) +
                        product.UKPrice.ToString("C2", CultureInfo.CreateSpecificCulture("en-GB")).PadRight(15) +
                        product.Office.Currency
                        );

            // Resetting colour if it matched, so next items remain default colour
            if (Console.ForegroundColor == ConsoleColor.Black) Console.ResetColor();

            listPos++;
        }

        // This is to navigate the list above
        if (scroll == true)
        {
            ConsoleKeyInfo cKey = Console.ReadKey();
            if (cKey.Key == ConsoleKey.UpArrow)
            {
                if (choice > 0) choice--;
                else choice = products.Count - 1;
            }
            else if (cKey.Key == ConsoleKey.DownArrow)
            {
                if (choice < products.Count - 1) choice++;
                else choice = 0;
            }
            // Fetching result by pressing enter
            else if (cKey.Key == ConsoleKey.Enter)
            {
                return products[choice].ID;
            }
        }
    } while (scroll == true);
    Console.Write("\nPress enter to exit.");
    Console.ReadLine();
    if (scroll == true) return products[2].ID;
    Start();
    return -1;
}

static void DeleteItem(DemoDBContext context, int toDel)
{
    Products productToDel = context.Products.SingleOrDefault(x => x.ID == toDel);
    try
    {
        context.Products.Remove(productToDel);
        context.SaveChanges();
        Printings.SuccessMessage("Item has successfully been deleted.\nPress enter to return to the start.");
        Console.ReadLine();
        Start();
    }
    catch
    {
        Printings.ErrorMessage("Something broke. Did you try to edit an inexistent item?");
        Console.ReadLine();
    }

}
