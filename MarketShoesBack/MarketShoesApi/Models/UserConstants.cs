namespace MarketShoesApi.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel()
            {
                Username = "Vitaliy_admin",
                Email = "vitaliy@gmail.com",
                Password = "qwerty",
                GivenName = "Vitaliy",
                Surname = "Volovnik",
                Role = "Administrator"

            },
            new UserModel()
            {
                Username = "Maks_seller",
                Email = "Maks@gmail.com",
                Password = "qwerty1",
                GivenName = "Maks",
                Surname = "Mmmmm",
                Role = "Seller"

            }
        };
    }
}
