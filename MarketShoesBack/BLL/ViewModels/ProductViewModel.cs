using DLL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public decimal Price { get; set; }
        public string? Code { get; set; }
        public int Count { get; set; }
        public bool? IsAvailable { get; set; }
        public int? SellerId { get; set; }
        public List<IFormFile>? Photos { get; set; }
        //public List<SubCharacteristicViewModel>? Characteristics { get; set; }
    }
}
