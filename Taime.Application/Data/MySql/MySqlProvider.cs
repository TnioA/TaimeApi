using Microsoft.EntityFrameworkCore;
using Taime.Application.Data.MySql.Entities;

namespace Taime.Application.Data.MySql
{
    public class MySqlProvider : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AppointmentEntity> Appointments { get; set; }

        public MySqlProvider(DbContextOptions<MySqlProvider> options) : base(options) { }
    }
}