using Microsoft.EntityFrameworkCore;

namespace TaimeApi.Models
{
    public class Context : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }

        public Context(DbContextOptions<Context> options): base(options) {}
    }
}