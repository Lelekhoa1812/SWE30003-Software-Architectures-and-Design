using System;
using System.Xml.Linq;

namespace RIS
{
	public class Delivery
	{
        public DateTime DeliveryDate { get; set; }

        public Delivery(DateTime deliveryDate)
		{
            DeliveryDate = deliveryDate;
        }

        // Forward the order with details to the third party delivery service
        public void forwardOrderToThirdParty (string orderId, string customerName, string customerContact)
        {
            Console.WriteLine($"Order {orderId} received by delivery service at {DeliveryDate}.");
        }

        // Update delivery status
        public void updateDeliveryStatus(string orderId, string customerName, string customerContact)
        {
            Console.WriteLine("Delivery in progress...");
            // Wait time 5s to simulate an actual delivery wait-time
            Thread.Sleep(5000);
            // Successful delivery display
            Console.WriteLine($"Delivery #{orderId} has been successful. Customer {customerName} has received the order.");

        }

        // File will be saved to bin/Debug/net7.0 directory while using Visual Studio
        public void Save(string orderId, string customerName, string customerContact)
        {
            // Prepate file document to be saved
            string deliveryText = "Relaxing Koala Restaurant\n\n" +
                                 $"Delivery Invoice\n" +
                                 $"Order ID: #{orderId}\n" +
                                 $"Customer Name: {customerName}\n" +
                                 $"Contact Number: {customerContact}\n" +
                                 $"Date: {DeliveryDate}\n\n";

            string fileName = $"delivery{orderId}.txt";
            File.WriteAllText(fileName, deliveryText);
            Console.WriteLine($"Receipt saved as {fileName}");
        }
    }
}

