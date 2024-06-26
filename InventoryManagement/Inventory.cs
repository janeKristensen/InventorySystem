﻿using CsvHelper;
using System.Drawing;
using System.Globalization;
using System.Security.Policy;
using System.Xml.Linq;

namespace InventoryManagement
{
    public interface IListener
    {
        public void Update(Order order);
    }


    public sealed class Inventory : IListener
    {

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
            
            using (var db = new SubstanceContext())
            {
                foreach (var line in order.OrderDetails)
                {
                    var sub = db.ReferenceSubstances.Find(line.Substance.BatchNumber);
                    if (sub != null)
                    {
                        sub.Stock -= line.Amount;
                        db.SaveChanges();
                    }
                }
            };  
        }


        public static void RemoveSubstance(Substance substance)
        {
            using (var db = new SubstanceContext())
            {
                db.ReferenceSubstances.Attach(substance);
                db.ReferenceSubstances.Remove(substance);
                db.SaveChanges();
            };
        }


        // To be implemented with GUI
        // Take input from user and add to database
        public static void PressButtonForNewSubstance()
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
                Console.WriteLine("Enter amount in stock:");
                try
                {
                    stock = Convert.ToInt32(GetUserInput());
                }
                // Catch exception and continue to take input if input cannot be converted to int
                catch (FormatException)
                {
                    continue;
                }
            }

            Console.WriteLine("Enter substance type:");
            string type = GetUserInput();

            using (var db = new SubstanceContext())
            {
                // Add to database and save
                db.Add(new Substance(name, batch, size, stock, type));
                db.SaveChanges();
            };


            // Nested method to get user input
            static string GetUserInput()
            {
                // Continue polling for input until a string has been entered
                string input = string.Empty;
                while (string.IsNullOrEmpty(input))
                {
                    input += Console.ReadLine();
                }

                return input;
            }
        }


        public void PrintStock()
        {
            using (var db = new SubstanceContext())
            {
                var items = db.ReferenceSubstances.ToList();
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Name}, {item.BatchNumber}, {item.Unit}: {item.Stock}");
                }
            }
                
        }

    }
}
