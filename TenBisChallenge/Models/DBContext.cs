using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TenBisChallenge.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("name=TenBisMock")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DBContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<MoneyCard> MoneyCards { get; set; }
    }
}