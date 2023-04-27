using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public List<BasketElement> BasketElements { get; set; }
    }
}
