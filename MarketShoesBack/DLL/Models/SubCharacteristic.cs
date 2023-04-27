using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class SubCharacteristic
    {
        public int Id { get; set; }
        public string Name { get; set; }    

        public Characteristic Сharacteristic { get; set; }
        public int CharacteristicId { get; set; }
    }
}
