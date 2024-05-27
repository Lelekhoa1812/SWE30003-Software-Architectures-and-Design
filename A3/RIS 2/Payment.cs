using System;
using System.Collections.Generic;
using System.Linq;

namespace RIS
{
    public class Payment
    {
        public DateTime PaymentDate { get; set; }

        public Payment(DateTime paymentDate)
        {
            PaymentDate = paymentDate;
        }

        // Function to enter payment details and process the transaction
        public void MakePayment(string orderId, string customerName, string customerContact, List<MenuItem> cart)
        {
            string type = GetPaymentType();
            string cardnumber = GetCardNumber();
            string expiration = GetExpirationDate();
            string cvv = GetCVV();

            Console.WriteLine("Payment successfully processed.");
            Console.WriteLine($"Thank you for your order! Your order ID is #{orderId}");

            // Printing invoice and/or receipt after payment
            Console.WriteLine("Would you like to print the invoice and receipt?");
            string input = GetInvoiceAndReceipt();

            // Set invoice
            DateTime invoiceDate = PaymentDate;
            Invoice invoice = new Invoice(invoiceDate);

            // Set receipt
            DateTime receiptDate = PaymentDate;
            Receipt receipt = new Receipt(receiptDate);

            switch (input) // Print input will be as saving a text file with invoice, receipt (or both) information
            {
                case "invoice":
                    invoice.Save(orderId, customerName, customerContact, cart, type);
                    break;
                case "receipt":
                    receipt.Save(orderId, customerName, customerContact, cart, type);
                    break;
                case "both":
                    invoice.Save(orderId, customerName, customerContact, cart, type);
                    receipt.Save(orderId, customerName, customerContact, cart, type);
                    break;
            }
        }

        // Customer can either input the payment by the number of selection, or the method name
        private string GetPaymentType()
        {
            while (true)
            {
                Console.WriteLine("Choose your payment method:");
                Console.WriteLine("1. Credit card\n2. Debit card");
                Console.Write("> ");
                string type = Console.ReadLine().Trim().ToLower();

                if (type == "1" || type == "credit card")
                {
                    return "credit card";
                }
                else if (type == "2" || type == "debit card")
                {
                    return "debit card";
                }
                else // Return error when type not matching
                {
                    Console.WriteLine("Invalid payment method. Please choose a valid payment type.");
                }
            }
        }

        // Card number must be 16 characters (length = 16), and they must be digits each (0-9)
        private string GetCardNumber()
        {
            while (true)
            {
                Console.WriteLine("Enter your payment details.");
                Console.WriteLine("Card number:");
                Console.Write("> ");
                string cardnumber = Console.ReadLine().Replace(" ", "");

                if (cardnumber.Length == 16 && cardnumber.All(char.IsDigit))
                {
                    return cardnumber;
                }
                else // Return error when card number not matching
                {
                    Console.WriteLine("Please enter valid card details.");
                }
            }
        }

        // Expiration date must be mm/yy format
        private string GetExpirationDate()
        {
            while (true)
            {
                Console.WriteLine("Expired date (mm/yy):");
                Console.Write("> ");
                string expiration = Console.ReadLine();

                if (DateTime.TryParseExact(expiration, "MM/yy", null, System.Globalization.DateTimeStyles.None, out _))
                {
                    return expiration;
                }
                else // Return error when expiration date not matching
                {
                    Console.WriteLine("Please enter a valid expiration date as mm/yy format.");
                }
            }
        }

        // CVV number must be 3 characters with all of them are digits (0-9)
        private string GetCVV()
        {
            while (true)
            {
                Console.WriteLine("CVV:");
                Console.Write("> ");
                string cvv = Console.ReadLine();

                if (cvv.Length == 3 && cvv.All(char.IsDigit))
                {
                    return cvv;
                }
                else // Return error when CVV not matching
                {
                    Console.WriteLine("Invalid input. Please enter 3 digits number from the back of your card.");
                }
            }
        }

        // Customer can get to choose either invoice, receipt, or both
        private string GetInvoiceAndReceipt()
        {
            while (true)
            {
                Console.WriteLine("1. Print Invoice\n2. Print Receipt\n3. Both");
                Console.Write("> ");
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "1" || input == "invoice" || input == "print invoice")
                {
                    return "invoice";
                }
                else if (input == "2" || input == "receipt" || input == "print receipt")
                {
                    return "receipt";
                }
                else if (input == "3" || input == "both")
                {
                    return "both";
                }
                else // Return error when input is not valid
                {
                    Console.WriteLine("Invalid request.");
                }
            }
        }
    }
}
