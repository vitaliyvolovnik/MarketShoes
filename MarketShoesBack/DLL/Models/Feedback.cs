using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public string Text { get; set; }
        public double Rating { get; set; }
        
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        
        public DateTime CreatedTime { get; set; }

        public List<string> Photos { get; set; } = new List<string>();
    }
}
