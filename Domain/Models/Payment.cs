﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using DataModels.Base;
using System;
using System.Collections.Generic;

namespace DataModels.Models;

public partial class Payment : BaseClass
{


    public string InvoiceId { get; set; }

    public decimal AmountPaid { get; set; }

    public string PaymentMethod { get; set; }

    public virtual Invoice? Invoice { get; set; }
}