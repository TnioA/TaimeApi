using Microsoft.EntityFrameworkCore;
using Taime.Application.Data.MySql.Entities;

namespace Taime.Application.Data.MySql
{
    public class MySqlContext : DbContext
    {
        public virtual DbSet<AddressEntity> Addresses { get; set; }
        public virtual DbSet<BrandEntity> Brands { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<CollectionEntity> Collections { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<ReviewEntity> Reviews { get; set; }
        public virtual DbSet<SkuEntity> Skus { get; set; }
        public virtual DbSet<TransactionAddressEntity> TransactionAddresses { get; set; }
        public virtual DbSet<TransactionEntity> Transactions { get; set; }
        public virtual DbSet<TransactionItemEntity> TransactionItems { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

        public MySqlContext() { }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); 
        }
    }
}