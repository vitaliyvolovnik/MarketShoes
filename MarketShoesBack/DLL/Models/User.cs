using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public bool IsEmailConfirm { get; set; }

        public string? Number { get; set; }

        public string? Password { get; set; }


        public Seller? Seller { get; set; }

        public Customer? Customer { get; set; }

    }
}
