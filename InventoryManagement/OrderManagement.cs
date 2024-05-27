

using CsvHelper;
using CsvHelper.Configuration;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;

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
            using (var db = new OrderContext())
            {
                var items = db.Orders.ToList();
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Address}");
                }
            }
        }

        // To be implemented with GUI
        // Create a new order and return
        public void PressButtonToAddOrder()
        {
            using (var db = new OrderContext())
            {
                var order = new Order("Industriparken 55, 2840 Ballerup");
                // Add to database and save
                db.Add(order);
                db.SaveChanges();

                // Add the order object to dictionary with Id as key 
                this.Notify(order);
            }; 
        }
    }
}
