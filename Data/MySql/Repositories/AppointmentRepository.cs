using TaimeApi.Data.MySql.Entities;
using TaimeApi.Utils.Attributes;
using TaimeApi.Utils.Data.MySql;

namespace TaimeApi.Data.MySql.Repositories
{
    [InjectionType(InjectionType.Scoped)]
    public class AppointmentRepository : MySqlRepositoryBase<AppointmentEntity>
    {
        public AppointmentRepository(MySqlProvider mySqlProvider) : base(mySqlProvider) { }
    }
}