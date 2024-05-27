using System;
using System.Collections.Generic;
using System.Linq;

namespace RIS
{
    public class Menu
    {
        // List menuItem
        public List<MenuItem> Items { get; set; }

        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public void AddMenuItem(MenuItem item)
        {
            Items.Add(item);
        }

        public void RemoveMenuItem(MenuItem item)
        {
            Items.Remove(item);
        }

        // Method to display the entire menu
        public void DisplayMenu()
        {
            for (int i = 0; i < Count; i++)
            {
                MenuItem item = Items[i];
                Console.WriteLine($"{i + 1}. {item.Name} - {item.Desc} - ${item.Price}");
            }
        }

        // Method to get a specific menu item by index
        public MenuItem GetMenuItem(int index)
        {
            if (index >= 0 && index < Count)
            {
                return Items[index];
            }
            else
            {
                return null;
            }
        }

        // Method to get the total number of Items on the menu
        public int Count
        {
            get { return Items.Count; }
        }
    }
}