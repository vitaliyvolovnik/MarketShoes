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

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUser(modelBuilder.Entity<User>());
            base.OnModelCreating(modelBuilder);
        }





        protected void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(x => x.Seller).WithOne(x => x.User).HasForeignKey<Seller>(x => x.UserId).IsRequired(false);
            builder.HasOne(x => x.Customer).WithOne(x => x.User).HasForeignKey<Customer>(x => x.UserId).IsRequired(false);
        }

    }
}
