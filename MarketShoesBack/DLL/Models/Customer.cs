﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public User? User { get; set; }

        public int UserId { get; set; }
        
        public Basket Basket { get; set; }


    }
}
