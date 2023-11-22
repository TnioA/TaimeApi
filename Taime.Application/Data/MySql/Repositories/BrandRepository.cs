using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    public class BrandRepository : MySqlRepositoryBase<BrandEntity>
    {
        public BrandRepository(MySqlContext mySqlContext) : base(mySqlContext) { }
    }
}