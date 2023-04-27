using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public List<SubCharacteristic> Characteristics { get; set; } = new List<SubCharacteristic>();
        public List<string> Photos { get; set; } = new List<string>();
        
        public Seller Seller { get; set; }

        public int SellerId { get; set; }
        
        public decimal Price { get; set; }

        public string Code { get; set; }
    }
}
