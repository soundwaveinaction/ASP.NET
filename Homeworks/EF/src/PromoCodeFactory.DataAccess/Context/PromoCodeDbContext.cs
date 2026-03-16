using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Persistance
{
    public class PromoCodeDbContext : DbContext
    {
        public DbSet<PromoCode> Promocodes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Preference> Preferences { get; set; }

        public PromoCodeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected PromoCodeDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
