﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using DataModels.Base;
using System;
using System.Collections.Generic;

namespace DataModels.Models;

public partial class Category:BaseClass
{
   

    public string Name { get; set; }

    public string Description { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}