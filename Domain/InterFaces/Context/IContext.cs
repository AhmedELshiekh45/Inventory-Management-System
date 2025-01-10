using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterFaces.Context
{
    public interface IContext
    {
        DbSet<Category> Categories { get; set; }

        DbSet<Invoice> Invoices { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<OrderDeatil> OrderDeatils { get; set; }

        DbSet<Payment> Payments { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<Stock> Stocks { get; set; }

        DbSet<StockProduct> StockProducts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
