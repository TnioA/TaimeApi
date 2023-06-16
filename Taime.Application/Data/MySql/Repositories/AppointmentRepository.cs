using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Attributes;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    [InjectionType(InjectionType.Scoped)]
    public class AppointmentRepository : MySqlRepositoryBase<AppointmentEntity>
    {
        public AppointmentRepository(MySqlProvider mySqlProvider) : base(mySqlProvider) { }
    }
}