using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Characteristic
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCharacteristic> SubCharacteristics { get; set; }
    }
}
