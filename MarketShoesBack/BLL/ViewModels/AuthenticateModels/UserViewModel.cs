using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.AuthenticateModels
{
    public class UserViewModel
    {
        public int Id   { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public string JWToken { get; set; }

        public PersonalInfo PersonalInfo { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Role = user.Role;
            PersonalInfo = user.PersonalInfo;
        }


    }
}
