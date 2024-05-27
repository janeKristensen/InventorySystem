using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace InventoryManagement
{
    public class SubstanceContext : DbContext
    {
        public DbSet<Substance> ReferenceSubstances { get; set; }


        // Class is implemented as a singleton
        private static SubstanceContext? _instance;

        public static SubstanceContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SubstanceContext();
            }
            return _instance;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"Data Source =.\\SQLEXPRESS; Integrated Security = true; AttachDbFilename = .\\Substances.mdf; User Instance = true;Database = Substances.ReferenceSubstances;");
    }
}



    
