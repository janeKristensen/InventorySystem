using System;

namespace InventoryManagement
{

    public class Substance
    {
        public Substance(string Name, string BatchNumber, string VialSize, int Stock)
        {
            this.Name = Name;
            this.BatchNumber = BatchNumber;
            this.VialSize = VialSize;
            this.Stock = Stock;
        }

        public void addStock(int amount)
        {
            Stock += amount;
        }

        public void subtractStock(int amount)
        {
            Stock -= amount;
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
    }
}
