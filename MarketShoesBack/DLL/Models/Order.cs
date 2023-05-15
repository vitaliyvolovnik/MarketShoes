using DLL.Models.Enums;
namespace DLL.Models
{
    public class Order
    {
        public int Id { get; set; }

        public OrderState State { get; set; }

        public List <BasketElement> BasketElement { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public Seller Seller { get; set; }
        public int SellerId { get; set; }   


    }
}
