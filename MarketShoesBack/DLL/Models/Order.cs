using DLL.Models.Enums;
namespace DLL.Models
{
    public class Order
    {
        public int Id { get; set; }

        public OrderStatus State { get; set; }

        public List <OrderItem>? OrderItems { get; set; }

        public string Address { get; set; }

        public User? Seller { get; set; }
        public int? SellerId { get; set; }

        public User? Customer { get; set; }
        public int? CustomerId { get; set; }
    }
}
