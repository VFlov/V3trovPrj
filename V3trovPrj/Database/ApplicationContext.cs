using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using V3trovPrj.Models;

namespace V3trovPrj.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Software> Softwares { get; set; } = null!;
        public DbSet<Categories> Categories { get; set; } = null!;  

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=94.131.104.139;user=twister;password=twisterkfc;database=softyk;", ServerVersion.AutoDetect("server=94.131.104.139;user=twister;password=twisterkfc;database=softyk;"));
          ;
        }
    }

}
