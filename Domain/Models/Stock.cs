﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using DataModels.Base;
using System;
using System.Collections.Generic;

namespace DataModels.Models;

public partial class Stock : BaseClass
{


    public int StockId { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public virtual ICollection<StockProduct>? StockProducts { get; set; } = new List<StockProduct>();
    public virtual ICollection<User>? Employees { get; set; } = new List<User>();
}