using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;


namespace InventoryManagement
{
    public class Order
    {
        public Order(string address, (string Name, string Size, int Amount)[] substances)
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
            get; set;
        }

        public (string, string, int)[] SubstanceList
        {
            get; set;
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

    public sealed class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Map(m => m.Id).Index(1);
            Map(m => m.Address).Index(2);
            Map(m => m.SubstanceList).Index(3);
        }
    }
}
