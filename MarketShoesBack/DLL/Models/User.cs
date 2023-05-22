using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public bool IsEmailConfirm { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
        
        public string? Role { get; set; }

        public PersonalInfo PersonalInfo { get; set; }

        public List<UserToken> UserTokens { get; set; }


        //Seller Properties

        public List<Product> Products { get; set; }

        public List<Order> OrdersAsSeller { get; set; }
        



        //Customer properties

        public List<BasketItem> Basket { get; set; }

        public List<Order> OrdersAsCustomer { get; set; }

        public List<Feedback> Feedbacks { get; set; }


    }
}
