﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using DataModels.Base;
using System;
using System.Collections.Generic;

namespace DataModels.Models;

public partial class Product:BaseClass
{


    public string Name { get; set; }

    public string Description { get; set; }

    public float UnitPrice { get; set; }

    public int UnitsInStock { get; set; }

    public string CategoryId { get; set; }


    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDeatil>? OrderDeatils { get; set; } = new List<OrderDeatil>();

    public virtual ICollection<StockProduct>? StockProducts { get; set; } = new List<StockProduct>();
}