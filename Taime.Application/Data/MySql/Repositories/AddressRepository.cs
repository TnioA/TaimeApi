using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    public class AddressRepository : MySqlRepositoryBase<AddressEntity>
    {
        public AddressRepository(MySqlContext mySqlContext) : base(mySqlContext) { }
    }
}