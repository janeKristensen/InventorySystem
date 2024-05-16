using CsvHelper;
using System.Globalization;


namespace InventoryManagement
{
    public class Program
    {
       
        static void Main(string[] args)
        {
            // Singleton instance of inventory and Ordermanagement
            Inventory inventory = Inventory.GetInstance();
            OrderManagement orderManagement = OrderManagement.GetInstance();

            // Inventory will subscribe to Ordermanagement to receive notifications on new orders
            orderManagement.Attach(inventory);

            // Load inventory stock and orders from csv file 
            orderManagement.LoadOrders();
            inventory.LoadStock();
            Console.WriteLine("\nStock loaded in from file:\n");
            inventory.PrintStock();

            // Add a new substance by user input - GUI implementation to be made
            inventory.PressButtonForNewSubstance();

            // Add a new order - GUI implementation to be made
            var order = orderManagement.PressButtonToAddOrder();
            Console.WriteLine("\nData from order:");
            order.PrintOrder();
            
            // Print stock to terminal and save to csv file
            Console.WriteLine("\nStock after order has been processed:\n");
            inventory.PrintStock();
            inventory.SaveStock();
            orderManagement.SaveOrders();
        }        
    }
}