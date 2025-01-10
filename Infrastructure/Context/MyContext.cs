using DataModels.Base;
using DataModels.Models;
using Domain.InterFaces.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Context
{
    public class MyContext : IdentityDbContext<User> ,IContext
    {
        public MyContext(DbContextOptions<MyContext> contextOptions) : base(contextOptions) { }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDeatil> OrderDeatils { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Stock> Stocks { get; set; }

        public virtual DbSet<StockProduct> StockProducts { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void HandleTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseClass>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Id = Guid.NewGuid().ToString();
                    entry.Entity.CreatedDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.Now;
                }
            }
        }
    }
}
