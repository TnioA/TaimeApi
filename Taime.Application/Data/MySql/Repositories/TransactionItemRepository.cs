using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    public class TransactionItemRepository : MySqlRepositoryBase<TransactionItemEntity>
    {
        public TransactionItemRepository(MySqlContext mySqlContext) : base(mySqlContext) { }
    }
}