using System;
using System.Collections.Generic;

namespace RIS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hard-code menuItem data as name, type, description and price
            Menu menu = new Menu();
            menu.AddMenuItem(new MenuItem("Spring rolls", "Rice paper wrapping shredded veggie, meat or seafood", 8.00));
            menu.AddMenuItem(new MenuItem("Dumpling", " Pieces of cooked dough often wrapped around a filling", 12.00));
            menu.AddMenuItem(new MenuItem("Carbonara", "Pasta made with eggs, cheese, guanciale (bacon), and black pepper", 18.00));
            menu.AddMenuItem(new MenuItem("Pho", "Vietnamese soup dish consisting of broth, rice noodles, herbs, and meat", 20.00));
            menu.AddMenuItem(new MenuItem("Tartare", "Raw ground beef served with onions, capers, mushrooms, pepper", 18.00));
            menu.AddMenuItem(new MenuItem("Cake", "Cheese, Chocolate and Strawberry cake", 14.00));
            menu.AddMenuItem(new MenuItem("Ice cream", "Mango, Coconut and Mint flavour", 6.00));
            menu.AddMenuItem(new MenuItem("Coffee", "Latte, Flat white, Cappuccino and Mocha", 6.00));
            menu.AddMenuItem(new MenuItem("Smoothie", "Tropical, Avocado, Watermelon, Banana", 8.00));

            // Hard-code table data as id, seatingCapacity value
            List<Table> tables = new List<Table>
            {
                new Table(1, 4),
                new Table(2, 4),
                new Table(3, 6),
                new Table(4, 2)
            };

            Console.WriteLine("Welcome to the Relaxing Koala Restaurant!");

            // Customer interaction loop
            while (true)
            {
                // Request choice
                Console.WriteLine("\nHow may I help you today? Enter the number as your request.");
                Console.WriteLine("1. Make reservation");
                Console.WriteLine("2. Place order");
                Console.WriteLine("3. Browse menu");
                Console.WriteLine("4. Exit");

                Console.Write("> "); // Add visual indicator for customer input

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number:");
                    Console.Write("> ");
                }

                switch (choice)
                {
                    case 1:
                        // When a customer makes a reservation, Customer creates an instance of Reservation class
                        new Customer("", "").MakeReservation(tables);
                        break;
                    case 2:
                        // When the customer places an order, Customer creates an instance of the Order class
                        new Customer("", "").PlaceOrder(menu);
                        break;
                    case 3:
                        // Display menu
                        Console.WriteLine("\nMenu:");
                        menu.DisplayMenu();
                        break;
                    case 4:
                        // Exit the program
                        Console.WriteLine("Thank you for visiting the Relaxing Koala Restaurant!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

        }
    }
}