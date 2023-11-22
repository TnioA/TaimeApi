using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    public class ReviewRepository : MySqlRepositoryBase<ReviewEntity>
    {
        public ReviewRepository(MySqlContext mySqlContext) : base(mySqlContext) { }
    }
}