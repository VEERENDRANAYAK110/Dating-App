using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<AppUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:datingapp-sqldb.database.windows.net,1433;Initial Catalog=DatingApp;Persist Security Info=False;User ID=datingapp;Password=SANDHYAveeru@143;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DatingApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
    }
}