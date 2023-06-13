using DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Context
{
    public class MarketShoesContext : DbContext
    {
        public MarketShoesContext(DbContextOptions<MarketShoesContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<BasketItem> BasketElements { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubCharacteristic> SubCharacteristics { get; set; }
        public DbSet<Characteristic> Сharacteristics { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurePersonalInfo(modelBuilder.Entity<PersonalInfo>());
            ConfigureUser(modelBuilder.Entity<User>());
            ConfigureProduct(modelBuilder.Entity<Product>());
            ConfigureCharacteristic(modelBuilder.Entity<Characteristic>());
            ConfigurUserToken(modelBuilder.Entity<UserToken>());


            base.OnModelCreating(modelBuilder);
        }


        protected void ConfigurePersonalInfo(EntityTypeBuilder<PersonalInfo> builder)
        {
            builder.HasKey(x => x.UserId);
            
            builder
                .HasOne(x => x.User)
                .WithOne(x => x.PersonalInfo)
                .HasForeignKey<PersonalInfo>(x => x.UserId)
                .IsRequired(true);
        }

        protected void ConfigureUser(EntityTypeBuilder<User> builder)
        {

            builder
                .HasMany(x => x.Products)
                .WithOne(x => x.Seller)
                .HasForeignKey(x => x.SellerId)
                .IsRequired();
            builder
                .HasMany(x => x.OrdersAsSeller)
                .WithOne(x => x.Seller)
                .HasForeignKey(x => x.SellerId)
                .IsRequired();

            builder
                .HasMany(x => x.OrdersAsCustomer)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder
                .HasMany(x => x.Basket)
                .WithOne(x=>x.Customer)
                .HasForeignKey(x=>x.CustomerId)
                .IsRequired();

            builder.HasMany(x => x.Feedbacks)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            

            
        }

        protected void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.HasMany(x => x.Feedbacks)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .IsRequired();

            builder
                .HasMany(x => x.Characteristics)
                .WithMany(x=>x.Products);


            
        }

        protected void ConfigurUserToken(EntityTypeBuilder<UserToken> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserTokens)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }

        protected void ConfigureCharacteristic(EntityTypeBuilder<Characteristic> builder)
        {
            builder
                .HasMany(x=>x.SubCharacteristics)
                .WithOne(x=>x.Characteristic)
                .HasForeignKey(x=>x.CharacteristicId)
                .IsRequired();
        }

        protected void ConfigurBasketItem(EntityTypeBuilder<BasketItem> builder)
        {
            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .IsRequired(false);
        }

    }
}
