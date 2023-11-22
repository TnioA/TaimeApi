using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    public class CategoryRepository : MySqlRepositoryBase<CategoryEntity>
    {
        public CategoryRepository(MySqlContext mySqlContext) : base(mySqlContext) { }
    }
}