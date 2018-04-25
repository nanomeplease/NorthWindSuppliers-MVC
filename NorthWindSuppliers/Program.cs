using System;
using System.Collections.Generic;
using DataLayer;
using DataLayer.Models;

namespace NorthWindSuppliers
{
    //Presentation Layer.
    class Program
    {
        static void Main(string[] args)
        {
            //Runs our Menu() on startup.
            while (true)
            {
                Menu();
            }
        }

        /// <summary>
        /// Menu to take the user through all actions.
        /// </summary>
        static void Menu()
        {
            //Instantiating objects.
            SupplierDAO supplierDAO = new SupplierDAO();
            SupplierDO supplier = new SupplierDO();

            //Loop to keep menu selection going.
            do
            {
                //Try catch to get any exeptions that may occur.
                try
                {
                    //Menu Options.
                    Console.Clear();
                    Console.WriteLine("\t\tPlease select option by pressing a key:\n");
                    Console.WriteLine(" 1) Display all");
                    Console.WriteLine(" 2) Create new");
                    Console.WriteLine(" 3) Update existing");
                    Console.WriteLine(" 4) Delete");
                    Console.WriteLine(" 5) Exit Application");

                    //Allows user to press either number keys on keyboard or use num pad.
                    ConsoleKeyInfo keyPressed = Console.ReadKey();
                    Console.Clear();

                    //Declaring variables.
                    int supplierId = 0;

                    //Creates a new list object called items to store our list object returned from our ViewAllSuppliers().
                    List<SupplierDO> items = supplierDAO.ViewAllSuppliers();

                    //Displays an updated supplier list.
                    DisplaySuppliers(items);

                    //Determines which key was pressed.
                    switch (keyPressed.Key)
                    {
                        //Display---------------------------------------------------------------------
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            break;

                        //Create----------------------------------------------------------------------
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            //Passing the object returned from GetUserInput method into the parameters for our CreateNewSuppliers().                
                            supplierDAO.CreateNewSuppliers(GetUserInput());
                            break;

                        //Update----------------------------------------------------------------------
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            Console.WriteLine("Please enter supplier Id for the row you would like to change.");
                            //Getting the supplier Id that user wants to update.
                            int.TryParse(Console.ReadLine(), out supplierId);

                            //Adding information returned from GetUserInput() into our object.
                            supplier = GetUserInput();

                            //Setting the value of our object variable for supplierId.
                            supplier.SupplierId = supplierId;

                            //Passing all the user input into UpdateSuppliers() using our object named supplier.
                            supplierDAO.UpdateSuppliers(supplier);
                            break;

                        //Delete----------------------------------------------------------------------
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            Console.WriteLine("Please enter supplier Id.");

                            //Getting supplier Id that user wants to delete.
                            int.TryParse(Console.ReadLine(), out supplierId);

                            //Setting the value of our object variable for supplierId.
                            supplier.SupplierId = supplierId;

                            //Passing supplier Id to the DeleteSuppliers() so that user can select which row to delete.
                            supplierDAO.DeleteSuppliers(supplier.SupplierId);
                            break;

                        //Exit-------------------------------------------------------------------------
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:

                            //Exiting the console.
                            Environment.Exit(0);
                            break;

                        default:
                            break;
                    }

                    //Displays updated supply list if user does NOT press 1.
                    if (keyPressed.Key != ConsoleKey.NumPad1 && keyPressed.Key != ConsoleKey.D1)
                    {
                        items = supplierDAO.ViewAllSuppliers();
                        DisplaySuppliers(items);
                    }
                }
                //Catches any exception that gets thrown.
                catch (Exception ex)
                {
                    //Prints error to console, logs, and restarts the menu.                    
                    supplierDAO.ErrorHandler(ex);
                    continue;
                }
            } while (true);
        }

        /// <summary>
        /// Gets required information to make changes from the user.
        /// </summary>
        /// <param name="contactTitle"></param>
        /// <param name="postalCode"></param>
        /// <param name="country"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        private static SupplierDO GetUserInput()
        {
            SupplierDO supplier = new SupplierDO();
            //Try catch to get any exeptions that may occur.            
            do
            {
                try
                {
                    //Collect information from the user.
                    Console.WriteLine("Please enter contact name.");
                    supplier.ContactName = Console.ReadLine();
                    Console.WriteLine("Please enter contact title.");
                    supplier.ContactTitle = Console.ReadLine();
                    Console.WriteLine("Please enter Postal Code.");
                    int.TryParse(Console.ReadLine(), out int postalCode);
                    Console.WriteLine("Please enter Country.");
                    supplier.Country = Console.ReadLine();
                    Console.WriteLine("Please enter Phone number");
                    supplier.PhoneNumber = Console.ReadLine();
                    supplier.PostalCode = postalCode.ToString();

                    //Returning object back to who called it.
                    return supplier;
                }
                catch (Exception ex)
                {
                    //Prints error to console and logs.
                    SupplierDAO supplierDAO = new SupplierDAO();
                    supplierDAO.ErrorHandler(ex);
                    continue;
                }
            }
            while (true);
        }

        /// <summary>
        /// Displays supplier information to the user.
        /// </summary>
        /// <param name="supplyInfo"></param>
        static void DisplaySuppliers(List<SupplierDO> items)
        {
            //Prints all rows.
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine($"SupplierID: { items[i].SupplierId}---| Name: {items[i].ContactName} | Title: " +
                        $"{  items[i].ContactTitle}\n Postal Code: { items[i].PostalCode} | Country: " +
                        $"{items[i].Country} | Phone number: {items[i].PhoneNumber}");
                }
            }
            Console.WriteLine("\n\t\t\tPress any key to continue.");
            Console.ReadKey();
        }
    }
}
