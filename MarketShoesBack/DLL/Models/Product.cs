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


        public string Model { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }


        public List<SubCharacteristic> Characteristics { get; set; } = new List<SubCharacteristic>();


        public List<Photo> Photos { get; set; } = new List<Photo>();
        
        public User Seller { get; set; }
        public int SellerId { get; set; }
        
        public decimal Price { get; set; }

        public string Code { get; set; }

        public int Count { get; set; }

        public bool IsAvailable { get; set; }

        public List<Feedback> Feedbacks { get; set; }

    }
}
