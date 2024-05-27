using System;
using System.Configuration;
using InventoryManagement;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagement
{
    public class SubstanceContext : DbContext
    {
        public DbSet<Substance> ReferenceSubstances { get; set; }

        public SubstanceContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"Data Source ={ConfigurationManager.AppSettings["server"]}; Integrated Security = SSPI; User Instance = false; Database = Substances;");
    }
}
//C:\\Program Files\\Microsoft SQL Server\\MSSQL16.SQLEXPRESS\\MSSQL\\DATA\\Substances.mdf
//$"Data Source ={ConfigurationManager.AppSettings["server"]}; TrustServerCertificate=True; Integrated Security = true; AttachDbFilename = {ConfigurationManager.AppSettings["database"]};"
