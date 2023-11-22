using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    public class TransactionAddressRepository : MySqlRepositoryBase<TransactionAddressEntity>
    {
        public TransactionAddressRepository(MySqlContext mySqlContext) : base(mySqlContext) { }
    }
}