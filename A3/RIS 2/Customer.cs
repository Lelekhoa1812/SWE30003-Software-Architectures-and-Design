using System;
using System.Collections.Generic;

namespace RIS
{
    // Static class setting the orderId variable with incrementing sequence 
    public static class OrderIdGenerator
    {
        private static int currentOrderId = 0;

        public static string GetNextOrderId()
        {
            currentOrderId = (currentOrderId % 999) + 1; // Increment and wrap around at 999
            return currentOrderId.ToString("D3"); // Format as a three-digit string (i.e. 001 to 999)
        }
    }

    // Customer class include name and number (contact info)
    public class Customer
    {
        public string Name { get; private set; }
        public string Contact { get; private set; }

        public Customer(string name, string contact)
        {
            Name = name;
            Contact = contact;
        }

        // Method to enter customer's name 
        public void EnterName()
        {
            while (true)
            {
                Console.WriteLine("Enter your name:");
                Console.Write("> ");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    Name = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Name cannot be empty.");
                }
            }
        }

        // Method to enter customer's contact number 
        public void EnterContact()
        {
            while (true)
            {
                Console.WriteLine("Enter your contact number (10 digits):");
                Console.Write("> ");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && input.Length == 10 && input.All(char.IsDigit))
                {
                    Contact = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Contact number must be 10 digits and cannot be empty.");
                }
            }
        }


        // Method to place an order
        public void PlaceOrder(Menu menu)
        {
            // Enter customer details if not already entered
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Contact))
            {
                EnterName();
                EnterContact();
            }

            Console.WriteLine($"Hello {Name}!");

            // Display menu
            Console.WriteLine("\nMenu:");
            menu.DisplayMenu();

            // Cart to store selected items
            List<MenuItem> cart = new List<MenuItem>();

            while (true)
            {
                Console.WriteLine("Enter 'add' to add an item to your cart, 'remove' to remove an item, 'view' to view your cart, 'menu' to view menu, or 'done' to finish:");
                Console.Write("> ");
                string input = Console.ReadLine().Trim().ToLower();

                switch (input)
                {
                    case "add":
                        AddToCart(menu, cart);
                        break;
                    case "remove":
                        RemoveFromCart(cart);
                        break;
                    case "view":
                        ViewCart(cart);
                        break;
                    case "menu":
                        Console.WriteLine("\nMenu:");
                        menu.DisplayMenu();
                        break;
                    case "done":
                        if (cart.Count == 0)
                        {
                            Console.WriteLine("No items were selected. Order canceled.");
                        }
                        else
                        {
                            // Create an order with the items in the cart
                            string orderId = OrderIdGenerator.GetNextOrderId(); // Generate the next order ID
                            Order order = new Order(orderId, DateTime.Now); // Initialise new order
                            Payment payment = new Payment(DateTime.Now); // Initialise new payment
                            Delivery delivery = new Delivery(DateTime.Now); // Initialise new delivery

                            foreach (var item in cart)
                            {
                                order.AddItem(item);
                            }

                            // Display order summary
                            order.DisplayOrder();
                            order.Save();
                            // Proceed to payment
                            payment.MakePayment(orderId, Name, Contact, cart);
                            // Proceed to delivery
                            delivery.forwardOrderToThirdParty(orderId, Name, Contact);
                            delivery.updateDeliveryStatus(orderId, Name, Contact);
                            delivery.Save(orderId, Name, Contact);
                        }
                        return;
                    default:
                        Console.WriteLine("Invalid input. Please enter 'add', 'remove', 'view', 'menu', or 'done':");
                        break;
                }
            }
        }

        // Method to add an item to the cart
        private void AddToCart(Menu menu, List<MenuItem> cart)
        {
            Console.WriteLine("Enter the number of the item you want to add:");
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int itemNumber) && itemNumber >= 1 && itemNumber <= menu.Count)
            {
                MenuItem selectedItem = menu.GetMenuItem(itemNumber - 1);
                cart.Add(selectedItem);
                Console.WriteLine($"Added '{selectedItem.Name}' to your cart.");
            }
            else
            {
                Console.WriteLine("Invalid item number.");
            }
        }

        // Method to remove an item from the cart
        private void RemoveFromCart(List<MenuItem> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }

            Console.WriteLine("Enter the number of the item you want to remove:");
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int itemNumber) && itemNumber >= 1 && itemNumber <= cart.Count)
            {
                MenuItem removedItem = cart[itemNumber - 1];
                cart.RemoveAt(itemNumber - 1);
                Console.WriteLine($"Removed '{removedItem.Name}' from your cart.");
            }
            else
            {
                Console.WriteLine("Invalid item number.");
            }
        }

        // Method to view the items in the cart
        private void ViewCart(List<MenuItem> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
            }
            else
            {
                Console.WriteLine("\nYour Cart:");
                for (int i = 0; i < cart.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {cart[i].Name} -  ${cart[i].Price:F2}");
                }

                Console.WriteLine($"Total: ${cart.Sum(item => item.Price):F2}");
            }
        }


        // Method to make a reservation with validation
        public void MakeReservation(List<Table> tables)
        {
            // Enter customer details if not already entered
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Contact))
            {
                EnterName();
                EnterContact();
            }

            Console.WriteLine($"Hello {Name}!");
            Console.WriteLine("To make a reservation, please enter the desired seating capacity:");
            Console.Write("> ");

            int seatingCapacity;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out seatingCapacity) && seatingCapacity > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer for seating capacity:");
                    Console.Write("> ");
                }
            }

            Console.WriteLine("Enter reservation time (dd/MM/yyyy HH:mm):");
            Console.Write("> ");
            DateTime reservationDate;
            while (true)
            {
                string input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out reservationDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid date and time in the format dd/MM/yyyy HH:mm:");
                    Console.Write("> ");
                }
            }

            // Get available tables using modified method
            List<Table> availableTables = Reservation.FindAvailableTables(tables, Reservation.LoadReservations(), seatingCapacity, reservationDate);

            if (availableTables.Count == 0)
            {
                Console.WriteLine("No available table for the requested time and capacity.");
                return;
            }

            Console.WriteLine("Available Tables:");
            foreach (var table in availableTables)
            {
                Console.WriteLine($"Table Number: {table.TableNumber}, Seating Capacity: {table.SeatingCapacity}");
            }

            Console.WriteLine("Choose a table by entering its number:");
            Console.Write("> ");
            int tableNumber;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out tableNumber) && availableTables.Any(t => t.TableNumber == tableNumber))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please choose a valid table number:");
                    Console.Write("> ");
                }
            }

            Table chosenTable = availableTables.First(t => t.TableNumber == tableNumber);
            int reservationId = Reservation.LoadReservations().Count + 1;
            Reservation reservation = new Reservation(reservationId, reservationDate, chosenTable.TableNumber);
            reservation.Save();
            chosenTable.Reserve();
            Console.WriteLine($"Table {chosenTable.TableNumber} reserved successfully. Your reservation ID is {reservationId}");
        }



    }
}