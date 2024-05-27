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

            // Add a new substance by user input - GUI implementation to be made
            //inventory.PressButtonForNewSubstance();

            // Add a new order - GUI implementation to be made
            orderManagement.PressButtonToAddOrder();
            inventory.PrintStock();
            orderManagement.PrintOrders();  
        }        
    }
}