using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;


namespace InventoryManagement
{
    public class Order
    {
        public Order(string Address, Substance[] SubstanceList)
        {
            // Generating random unique id number 
            string hashString = Convert.ToString(DateTime.UtcNow);
            this.Id = hashString.GetHashCode();
            this.Address = Address;
            this.SubstanceList = SubstanceList;  
        }

        public int Id
        {
            get;
        }

        public string Address
        {
            get; private set;
        }

        public Substance[] SubstanceList
        {
            get; private set;
        }

        public void PrintOrder()
        {
            Console.WriteLine($"{this.Address}\nSubstance order:");
            foreach (var substance in this.SubstanceList)
            {
                Console.WriteLine($"{substance.Name} {substance.Unit}: {substance.Stock} pcs.");
            }
        }
    }
}
