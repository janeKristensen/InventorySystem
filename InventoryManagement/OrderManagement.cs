

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
        private Dictionary<int, Order> _orders = new Dictionary<int, Order>();
        private List<IListener> _listeners = new List<IListener>();

        // Class is implemented as a singleton
        private static OrderManagement? _orderManager;

        public static OrderManagement GetInstance() {
            if (_orderManager == null)
            {
                _orderManager = new OrderManagement();
            }
            return _orderManager;
        }

        public void PrintOrders()
        {
            Console.WriteLine(_orders.ToString());
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

        // To be implemented with GUI
        // Create a new order and return
        public Order PressButtonToAddOrder()
        {
            Order order = new Order(
               "Industriparken 55, 2840 Ballerup",
               new (string, string, int)[] {
                    ("Methylparahydroxy", "100mg/vial", 10),
                    ("Calcipotriol", "50mg/vial", 5),
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
                csv.Context.RegisterClassMap<OrderMap>();

                var records = new Dictionary<int, Order>();
                
               /* csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    (string, string, int)[] orderList = csv.GetField<(string, string, int)[]>(3);
                    var order = new Order
                    (
                        csv.GetField<string>("Address"),
                        orderList
                    ) ;
                    records.Add(csv.GetField<int>("Key"), order);
                    
                }*/
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
