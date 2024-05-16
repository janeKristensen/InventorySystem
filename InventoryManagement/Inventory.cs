using CsvHelper;
using System.Globalization;

namespace InventoryManagement
{
    public interface IListener
    {
        public void Update(Order order);
    }


    public sealed class Inventory : IListener
    {
        // Dict of all substances that has been added to inventory
        private Dictionary<string, Substance> _subDict = new Dictionary<string, Substance>();

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


        // Implementation of interface method update
        public void Update(Order order)
        {
            Console.WriteLine("\nOrder received in inventory!\n");

            // Locate substance in dictionary using key and subtract amount from stock
            (string Name, string VialSize, int Amount)[] substanceOrder = order.SubstanceList;
            foreach (var line in substanceOrder)
            {
                string key = (line.Name + line.VialSize);
                try
                {
                    var sub = _subDict[key];
                    sub.subtractStock(line.Amount);
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"Substance {line.Name}  {line.VialSize} was not found in inventory");
                }
            }
        }

        // To be implemented with GUI
        // Take input from user and return new substance
        public void PressButtonForNewSubstance()
        {
            Console.WriteLine("Enter substance name:");
            string name = GetUserInput();

            Console.WriteLine("Enter batch number:");
            string batch = GetUserInput();

            Console.WriteLine("Enter vial size:");
            string size = GetUserInput();

            int stock = 0;
            while (stock <= 0)
            {
                try
                {
                    Console.WriteLine("Enter amount in stock:");
                    stock = Convert.ToInt32(GetUserInput());
                }
                // Catch exception and continue to take input if input cannot be converted to int
                catch (FormatException)
                {
                    continue;
                }
            }

            string type = string.Empty;
            while (true)
            {
                Console.WriteLine("Enter substance type:");
                type = GetUserInput();
                if (type.ToUpper() == "RS" || type.ToUpper() == "IS")
                {
                    break;
                }
            }

            Substance substance;
            if (type == "RS")
            {
                substance = new ReferenceSubstance(name, batch, size, stock, type);
            }
            else
            {
                substance = new InternalStandard(name, batch, size, stock, type);
            }

            AddSubstance(substance);

            static string GetUserInput()
            {
                // Continue polling for input until a string has been entered
                string input = string.Empty;
                while (string.IsNullOrEmpty(input))
                {
                    input = Console.ReadLine();
                }

                return input;
            }
        }

        public void AddSubstance(Substance substance)
        {
            _subDict.Add(substance.Name + substance.VialSize, substance);
        }

        public void RemoveSubstance(Substance substance)
        {
            _subDict.Remove(substance.Name + substance.VialSize);
        }

        public Dictionary<string, Substance> GetInventoryList()
        {
            return _subDict;
        }

        public void PrintStock()
        {
            string output = string.Empty;
            foreach(var key in _subDict)
            {
                output += $"{key.Value.Name} {key.Value.VialSize}, batch {key.Value.BatchNumber}: {key.Value.Stock} vials\n";
            }

            Console.WriteLine(output);
        }

        public void LoadStock()
        {
            // Read from file path with CsvHelper library
            // 'using' keyword - to dispose of IDisposable objects when out of scope
            using (var reader = new StreamReader("D:\\Visual Studio stuff\\Projekts\\InventoryManagement\\stock.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    if(csv.GetField("Type") == "RS")
                    {
                        var records = csv.GetRecord<ReferenceSubstance>();
                        if (records != null)
                        {
                            _instance.AddSubstance(records);
                        }
                    }
                    else
                    {
                        var records = csv.GetRecord<InternalStandard>();
                        if (records != null)
                        {
                            _instance.AddSubstance(records);
                        }
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
                csv.WriteRecords(_subDict);
            }
        }
    }
}
