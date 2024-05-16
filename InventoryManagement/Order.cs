using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;


namespace InventoryManagement
{
    public class Order
    {
        public Order(string address, Substance[] substances)
        {
            // Generating random unique id number 
            string hashString = Convert.ToString(DateTime.UtcNow);
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

        public Substance[] SubstanceList
        {
            get; private set;
        }

        public void PrintOrder()
        {
            Console.WriteLine($"{Address}\n Substance order:");
            foreach (var substance in SubstanceList)
            {
                Console.WriteLine($"{substance.Name} {substance.VialSize}: {substance.Stock} pcs.");
            }
        }
    }

    public sealed class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Map(m => m.Id);
            Map(m => m.Address);
            Map(m => m.SubstanceList);
        }
    }
}
