using DLL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class UserToken
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public TokenType type { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}
