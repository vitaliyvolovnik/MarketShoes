using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class PersonalInfo
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public string? Number { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        

    }
}
