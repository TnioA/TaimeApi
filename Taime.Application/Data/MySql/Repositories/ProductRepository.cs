using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Attributes;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    public class ProductRepository : MySqlRepositoryBase<ProductEntity>
    {
        public ProductRepository(MySqlContext mySqlContext) : base(mySqlContext) { }
    }
}