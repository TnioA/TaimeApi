using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaimeApi.Data.MySql.Entities;

namespace TaimeApi.Data.MySql
{
    public class MySqlProvider : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AppointmentEntity> Appointments { get; set; }

        public MySqlProvider(DbContextOptions<MySqlProvider> options) : base(options) { }
    }
}