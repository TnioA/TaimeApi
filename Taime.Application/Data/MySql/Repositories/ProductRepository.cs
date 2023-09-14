﻿using Taime.Application.Data.MySql.Entities;
using Taime.Application.Utils.Attributes;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Repositories
{
    [InjectionType(InjectionType.Scoped)]
    public class ProductRepository : MySqlRepositoryBase<ProductEntity>
    {
        public ProductRepository(MySqlProvider mySqlProvider) : base(mySqlProvider) { }
    }
}