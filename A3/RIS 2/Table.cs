namespace RIS
{
    // Table include id, size, time (lunch/dinner), and occupied boolean data
    public class Table
    {
        public int TableNumber { get; set; }
        public int SeatingCapacity { get; set; }
        public bool IsAvailable { get; set; }

        public Table(int tableNumber, int seatingCapacity)
        {
            TableNumber = tableNumber;
            SeatingCapacity = seatingCapacity;
            IsAvailable = true; // Initialise as available
        }

        public bool CanAccommodate(int capacity)
        {
            return SeatingCapacity >= capacity;
        }

        public void Reserve()
        {
            IsAvailable = false;
        }
    }

}