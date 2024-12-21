using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinnApp_v4
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Budget> Budgets { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=budgets.db");
            //ptionsBuilder.UseSqlite
        }
    }
}
