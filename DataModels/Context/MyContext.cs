using DataModels.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Context
{
    public class MyContext : IdentityDbContext<User>
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
    }
}
