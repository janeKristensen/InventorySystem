using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    internal class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrderContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"Data Source ={ConfigurationManager.AppSettings["server"]}; Integrated Security = SSPI; User Instance = false; Database = Substances;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
        }
    }
}
