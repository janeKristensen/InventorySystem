using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace InventoryManagement
{
    public class Order
    {
        Substance[] SubstanceList;

        public Order(string Address )
        {
            this.Address = Address;
        }

        [Key]
        public int Id
        {
            get;
        }

        public string Address
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
