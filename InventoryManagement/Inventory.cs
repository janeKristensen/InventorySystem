using CsvHelper;
using System.Globalization;


namespace InventoryManagement
{
    public interface IListener
    {
        public void Update(Order order);
    }


    public class Inventory : IListener
    {
        // List of all substances that has been added to inventory
        private List<Substance> _substances = new List<Substance>();

        // Class is implemented as a singleton
        private static Inventory? _instance;

        public static Inventory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Inventory();
            }
            return _instance;
        }

        public void AddSubstance(Substance substance)
        {
            _substances.Add(substance);
        }

        public void RemoveSubstance(Substance substance)
        {
            _substances.Remove(substance);
        }

        public List<Substance> GetInventoryList()
        {
            return _substances;
        }

        public void PrintStock()
        {
            string output = string.Empty;
            foreach(Substance substance in _substances)
            {
                output += $"{substance.Name} {substance.VialSize}, batch {substance.BatchNumber}: {substance.Stock} vials\n";
            }

            Console.WriteLine(output);
        }

        // Implementation of interface method update
        public void Update(Order order)
        {
            Console.WriteLine("\nOrder received in inventory!\n");

            // 'Find' is a List<T> method to locate the first element in a list that matches 
            // This approach only works when there is no more than one active batch of a substance in the inventory
            (string Name, string VialSize, int Amount)[] substanceOrder = order.SubstanceList;
            foreach(var line in substanceOrder)
            {
                var sub = _substances.Find(
                    delegate (Substance substance) 
                    { 
                        return substance.Name == line.Name && substance.VialSize == line.VialSize; 
                    }
                    );
                if (sub != null) 
                {
                    sub.subtractStock(line.Amount);
                }
            }
        }

        public void LoadStock()
        {
            // Read from file path with CsvHelper library
            // 'using' keyword - to dispose of IDisposable objects when out of scope
            using (var reader = new StreamReader("D:\\Visual Studio stuff\\Projekts\\InventoryManagement\\stock.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                while (csv.Read())
                {
                    var records = csv.GetRecord<Substance>();
                    if (records != null)
                    {   
                        _instance.AddSubstance(records);
                    }
                }
            }
        }

        public void SaveStock()
        {
            // Write to file path with CsvHelper library
            // 'using' keyword - to dispose of IDisposable objects when out of scope
            using (var writer = new StreamWriter("D:\\Visual Studio stuff\\Projekts\\InventoryManagement\\stock.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // Write the list of substances stored in inventory instance
                csv.WriteRecords(_substances);
            }
        }
    }
}
