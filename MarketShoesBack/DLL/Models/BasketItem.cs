﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class BasketItem
    {
        public int Id { get; set; } 
        public Product Product { get; set; }
        public int Count { get; set; }
        public List<SubCharacteristic> SubCharacteristics { get; set; }

        public User? Customer { get; set; }
        public int? CustomerId { get; set; }



    }
}