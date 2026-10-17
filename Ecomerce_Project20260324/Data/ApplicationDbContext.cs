using Ecomerce_Project20260324.Configuration;
using Ecomerce_Project20260324.Models.AuthModels;
using Ecomerce_Project20260324.Models.Master;
using Ecomerce_Project20260324.Models.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce_Project20260324.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApiUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)   
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }


        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<BaseProduct> BaseProducts { get; set; }
        public DbSet<ImageBase> ImageBases { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Customer_Feedback> CustomerFeedbacks { get; set; }


    }
}
