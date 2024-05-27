using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RIS
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int TableNumber { get; set; }

        public Reservation(int reservationId, DateTime reservationDate, int tableNumber)
        {
            ReservationId = reservationId;
            ReservationDate = reservationDate;
            TableNumber = tableNumber;
        }

        public static bool IsTableAvailable(int tableNumber, DateTime reservationDate, List<Reservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                if (reservation.TableNumber == tableNumber &&
                    reservationDate >= reservation.ReservationDate &&
                    reservationDate < reservation.ReservationDate.AddHours(2)) // Assuming each reservation is 2 hours long
                {
                    return false;
                }
            }
            return true;
        }

        public static List<Table> FindAvailableTables(List<Table> tables, List<Reservation> reservations, int capacity, DateTime reservationDate)
        {
            List<Table> availableTables = new List<Table>();

            foreach (var table in tables)
            {
                if (table.CanAccommodate(capacity) && IsTableAvailable(table.TableNumber, reservationDate, reservations))
                {
                    availableTables.Add(table);
                }
            }

            return availableTables;
        }


        public void Save()
        {
            // Create directory if it doesn't exist
            Directory.CreateDirectory("data");

            // Save reservation details to a file
            try
            {
                File.AppendAllText("data/reservations.txt", $"{ReservationId},{ReservationDate},{TableNumber}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving reservation: {ex.Message}");
            }
        }


        public static List<Reservation> LoadReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            if (File.Exists("data/reservations.txt"))
            {
                var lines = File.ReadAllLines("data/reservations.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    int reservationId = int.Parse(parts[0]);
                    DateTime reservationDate = DateTime.Parse(parts[1]);
                    int tableNumber = int.Parse(parts[2]);
                    reservations.Add(new Reservation(reservationId, reservationDate, tableNumber));
                }
            }
            return reservations;
        }
    }
}