using DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Context
{
    public class MarketShoesContext : DbContext
    {
        public MarketShoesContext(DbContextOptions<MarketShoesContext> options) : base(options)
        {

            //Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketElement> BasketElements { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<SubCharacteristic> SubCharacteristics { get; set; }
        
        public DbSet<Characteristic> Сharacteristics { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUser(modelBuilder.Entity<User>());
            ConfigureProduct(modelBuilder.Entity<Product>());
            ConfigureBasket(modelBuilder.Entity<Basket>());
            ConfigureCharacteristic(modelBuilder.Entity<Characteristic>());


            base.OnModelCreating(modelBuilder);
        }





        protected void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(x => x.Seller)
                .WithOne(x => x.User)
                .HasForeignKey<Seller>(x => x.UserId)
                .IsRequired(false);

            builder
                .HasOne(x => x.Customer)
                .WithOne(x => x.User)
                .HasForeignKey<Customer>(x => x.UserId)
                .IsRequired(false);
        }

        protected void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasOne(x => x.Seller)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.SellerId)
                .IsRequired();
            builder
                .HasMany(x => x.Characteristics)
                .WithMany(x=>x.Products);
                
            
        }

        protected void ConfigureBasket(EntityTypeBuilder<Basket> builder)
        {
            builder
                .HasMany(x=>x.BasketElements)
                .WithOne(x=>x.Basket)
                .HasForeignKey(x=>x.BasketId)
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


    }
}
