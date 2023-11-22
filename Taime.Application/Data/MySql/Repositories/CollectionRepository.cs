using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    public class CollectionRepository : MySqlRepositoryBase<CollectionEntity>
    {
        public CollectionRepository(MySqlContext mySqlContext) : base(mySqlContext) { }
    }
}