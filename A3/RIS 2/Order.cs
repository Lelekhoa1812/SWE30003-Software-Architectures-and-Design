using System;
using System.Collections.Generic;
using System.IO;

namespace RIS
{
    public class Order
    {
        public string OrderId { get; set; }
        private List<MenuItem> Items { get; set; }
        public DateTime OrderDate { get; set; }

        public Order(string orderId, DateTime orderDate)
        {
            OrderId = orderId;
            Items = new List<MenuItem>();
            OrderDate = orderDate;
        }

        // Method to add a MenuItem to the order
        public void AddItem(MenuItem item)
        {
            Items.Add(item);
        }

        // Method to remove a MenuItem from the order
        public void RemoveItem(MenuItem item)
        {
            Items.Remove(item);
        }

        // Method to calculate the total price of the order
        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in Items)
            {
                total += item.Price;
            }
            return total;
        }

        // Method to display the order
        public void DisplayOrder()
        {
            Console.WriteLine("Order Summary:");
            foreach (var item in Items)
            {
                Console.WriteLine($"- {item.Name} - ${item.Price}");
            }
            Console.WriteLine($"Total: ${CalculateTotal():F2}");
        }


        public void Save()
        {
            try
            {
                Directory.CreateDirectory("data");
                string fileName = $"data/order_{OrderId}.txt"; // Example file name format: order_1.txt
                using StreamWriter writer = new StreamWriter(fileName);
                writer.WriteLine($"Order ID: {OrderId}");
                writer.WriteLine($"Order Date: {OrderDate}");
                writer.WriteLine("Items:");

                foreach (var item in Items)
                {
                    writer.WriteLine($"- {item.Name} - ${item.Price}");
                }

                double total = Items.Sum(i => i.Price);
                writer.WriteLine($"Total: ${total}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the order: {ex.Message}");
            }
        }
    }

}