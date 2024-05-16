

using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;

namespace InventoryManagement
{
    public interface IEventManager
    {
        public void Attach(IListener listener);
        public void Detach(IListener listener);
        public void Notify(Order order);
    }


    public sealed class OrderManagement : IEventManager
    {
        private Dictionary<int, Order> _orders = new();
        private List<IListener> _listeners = new();

        // Class is implemented as a singleton
        private static OrderManagement? _orderManager;

        public static OrderManagement GetInstance() {
            if (_orderManager == null)
            {
                _orderManager = new OrderManagement();
            }
            return _orderManager;
        }

        // Implementation of interface methods
        public void Attach(IListener listener)
        {
            _listeners.Add(listener);
        }

        public void Detach(IListener listener)
        {
            _listeners.Remove(listener);
        }

        public void Notify(Order order)
        {
            foreach (var listener in _listeners)
            {
                listener.Update(order);
            }
        }

        public void PrintOrders()
        {
            Console.WriteLine(_orders.ToString());
        }

        // To be implemented with GUI
        // Create a new order and return
        public Order PressButtonToAddOrder()
        {
            Order order = new(
               "Industriparken 55, 2840 Ballerup",
               new [] {
                    new Substance("Methylparahydroxy", "100mg/vial", 10),
                    new Substance("Calcipotriol", "50mg/vial", 5),
               });

            // Add the order object to dictionary with Id as key 
            _orders.Add(order.Id, order);
            Notify(order);

            return order;
        }

        public void LoadOrders()
        {
            // Read from file path with CsvHelper library
            // 'using' keyword - to dispose of IDisposable objects when out of scope

            using (var reader = new StreamReader("D:\\Visual Studio stuff\\Projekts\\InventoryManagement\\orders.csv")) 
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                while (csv.Read())
                {
                    var records = csv.GetRecord<Order>();
                    
                    if (records != null)
                    {
                        _orders.Add(records.Id, records);
                    }
                }
            }
        }

        public void SaveOrders()
        {
            // Write to file path with CsvHelper library
            // 'using' keyword - to dispose of IDisposable objects when out of scope
            using (var writer = new StreamWriter("D:\\Visual Studio stuff\\Projekts\\InventoryManagement\\orders.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<OrderMap>();
                // Write the list of substances stored in inventory instance
                csv.WriteRecords(_orders);
            }
        }
    }
}
