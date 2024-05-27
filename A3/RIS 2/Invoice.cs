using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RIS
{
    public class Invoice
    {
        public DateTime InvoiceDate { get; set; }

        public Invoice(DateTime invoiceDate)
        {
            InvoiceDate = invoiceDate;
        }

        // Customer's invoice will be saved as txt file, such as "invoice{orderId}.txt"
        // File will be saved to bin/Debug/net7.0 directory while using Visual Studio
        public void Save(string orderId, string customerName, string customerContact, List<MenuItem> cart, string type)
        {
            // Prepate file document to be saved
            string invoiceText = "Relaxing Koala Restaurant\n\n" +
                                 $"Customer Invoice\n" +
                                 $"Order ID: #{orderId}\n" +
                                 $"Customer Name: {customerName}\n" +
                                 $"Contact Number: {customerContact}\n" +
                                 $"Payment Method: {type}\n" +
                                 $"Date: {InvoiceDate}\n\n";

            for (int i = 0; i < cart.Count; i++) // List cart (menuItems) extracted from Customer class and append to invoiceText with prices and total
            {
                invoiceText += $"{i + 1}. {cart[i].Name} -  ${cart[i].Price:F2}\n";
            }
            invoiceText += $"Total: ${cart.Sum(item => item.Price):F2}\n";

            string fileName = $"invoice{orderId}.txt";
            File.WriteAllText(fileName, invoiceText);
            Console.WriteLine($"Invoice saved as {fileName}");
        }
    }
}