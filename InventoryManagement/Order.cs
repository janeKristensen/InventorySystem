using System;
using System.Collections.Generic;


namespace InventoryManagement
{
    public class Order
    {
        public Order(string address, (string Name, string Size, int Amount)[] substances)
        {
            // Generating random unique id number 
            string hashString = address + Convert.ToString(DateTime.UtcNow);
            Id = hashString.GetHashCode();
            Address = address;
            SubstanceList = substances;  
        }

        public int Id
        {
            get;
        }

        public string Address
        {
            get; private set;
        }

        public (string, string, int)[] SubstanceList
        {
            get; private set;
        }

        public void PrintOrder()
        {
            Console.WriteLine($"{Address}");
            foreach (var substance in SubstanceList)
            {
                Console.WriteLine(substance.ToString());
            }
        }
    }
}
