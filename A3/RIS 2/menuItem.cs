namespace RIS
{
    // menuItem include name, type, description and price
    public class MenuItem
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }

        public MenuItem(string name, string desc, double price)
        {
            Name = name;
            Desc = desc;
            Price = price;
        }

    }
}