using System;
using System.Collections.Generic;


namespace InventoryManagement
{
    public interface IEventManager
    {
        public void Attach(IListener listener);
        public void Detach(IListener listener);
        public void Notify(Order order);
    }


    public class OrderManagement : IEventManager
    {
        private Dictionary<int, Order> orders = new Dictionary<int, Order>();
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

        public void AddOrder(Order order)
        {
            // Add the order object to dictionary with Id as key 
            orders.Add(order.Id, order);
            Notify(order);
        }

        // Implementation of interface method 
        public void Attach(IListener listener)
        {
            _listeners.Add(listener);
        }

        // Implementation of interface method 
        public void Detach(IListener listener)
        {
            _listeners.Remove(listener);
        }

        // Implementation of interface method 
        public void Notify(Order order)
        {
            foreach (var listener in _listeners)
            {
                listener.Update(order);
            }
        }
    }
}
