using CsvHelper.Configuration;
using System;

namespace InventoryManagement
{
    public class Substance
    {
        public Substance(string Name, string BatchNumber, string VialSize, int Stock, string Type)
        {
            this.Name = Name;
            this.BatchNumber = BatchNumber;
            this.VialSize = VialSize;
            this.Stock = Stock;
            this.Type = Type;
        }

        public string Name
        {
            get; private set;
        }

        public string BatchNumber
        {
            get; private set;
        }

        public string VialSize
        {
            get; private set;
        }

        public int Stock
        {
            get; private set;
        }

        public string Type
        {
            get; private set;
        }

        public void addStock(int amount)
        {
            Stock += amount;
        }

        public void subtractStock(int amount)
        {
            Stock -= amount;
        }

        public virtual void DoTypeStuff()
        {
            Console.WriteLine("Doing substance stuff");
        }
        
    }



    public class ReferenceSubstance : Substance
    {
        public ReferenceSubstance(string Name, string BatchNumber, string VialSize, int Stock, string Type) : base(Name, BatchNumber, VialSize, Stock, Type) { }
  
        public override void DoTypeStuff()
        {
            Console.WriteLine("Doing Reference Substance stuff");
        }
    }



    public class InternalStandard : Substance
    {
        public InternalStandard(string Name, string BatchNumber, string VialSize, int Stock, string Type) : base(Name, BatchNumber, VialSize, Stock, Type) { }

        public override void DoTypeStuff()
        {
            Console.WriteLine("Doing Internal Standard stuff");
        }
    }
}


