using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RIS
{
    public class Receipt
    {
        public DateTime ReceiptDate { get; set; }

        public Receipt(DateTime receiptDate)
        {
            ReceiptDate = receiptDate;
        }

        // Customer's receipt will be saved as txt file, such as "receipt{orderId}.txt"
        // File will be saved to bin/Debug/net7.0 directory while using Visual Studio
        public void Save(string orderId, string customerName, string customerContact, List<MenuItem> cart, string type)
        {
            // Prepate file document to be saved
            string receiptText = "Relaxing Koala Restaurant\n\n" +
                                 $"Customer Receipt\n" +
                                 $"Order ID: #{orderId}\n" +
                                 $"Customer Name: {customerName}\n" +
                                 $"Contact Number: {customerContact}\n" +
                                 $"Payment Method: {type}\n" +
                                 $"Date: {ReceiptDate}\n\n";

            for (int i = 0; i < cart.Count; i++) // List cart (menuItems) extracted from Customer class and append to receiptText with price
            {
                receiptText += $"{i + 1}. {cart[i].Name} -  ${cart[i].Price:F2}\n";
            }
            receiptText += $"Total: ${cart.Sum(item => item.Price):F2}\n";

            string fileName = $"receipt{orderId}.txt";
            File.WriteAllText(fileName, receiptText);
            Console.WriteLine($"Receipt saved as {fileName}");
        }
    }
}