﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Management.Data.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public string Name { get; set; }
        public SqlMoney Price { get; set; }
        public decimal KdvRate { get; set; }
    }
}
