using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    public class TransactionRepository : MySqlRepositoryBase<TransactionEntity>
    {
        public TransactionRepository(MySqlContext mySqlContext) : base(mySqlContext) { }
    }
}