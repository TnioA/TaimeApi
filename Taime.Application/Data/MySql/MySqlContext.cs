using Microsoft.EntityFrameworkCore;
using Taime.Application.Data.MySql.Entities;

namespace Taime.Application.Data.MySql
{
    public class MySqlContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }

        public MySqlContext() { }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); 
        }
    }
}