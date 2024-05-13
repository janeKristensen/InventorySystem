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

            // Load inventory stock from csv file and print to terminal
            inventory.LoadStock();
            Console.WriteLine("Stock loaded in from file:\n");
            inventory.PrintStock();

            // Add a new substance by user input - GUI implementation to be made
            inventory.AddSubstance(PressButtonForNewSubstance());

            // Add a new order - GUI implementation to be made
            var order = PressButtonToAddOrder();
            Console.WriteLine("Data from order:\n");
            order.PrintOrder();
            orderManagement.AddOrder(order);

            // Print stock to terminal and save to csv file
            Console.WriteLine("Stock after order has been processed:\n");
            inventory.PrintStock();
            inventory.SaveStock();
        }



        // To be implemented with GUI
        // Take input from user and return new substance
        public static Substance PressButtonForNewSubstance()
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

            return new Substance(name, batch, size, stock);
        }

        public static string GetUserInput()
        {
            // Continue polling for input until a string has been entered
            string input = string.Empty;
            while (string.IsNullOrEmpty(input))
            {
                input = Console.ReadLine();
            }

            return input;
        }

        // To be implemented with GUI
        // Create a new order and return
        public static Order PressButtonToAddOrder()
        {
            Order order = new Order(
               "Industriparken 55, 2840 Ballerup",
               new (string, string, int)[] {
                    ("Methylparahydroxy", "100mg/vial", 10),
                    ("Calcipotriol", "50mg/vial", 5),
               });

            return order;
        }
    }
}