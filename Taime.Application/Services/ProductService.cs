using Taime.Application.Data.MySql.Entities;
using Taime.Application.Data.MySql.Repositories;
using Taime.Application.Enums;
using Taime.Application.Utils.Attributes;
using Taime.Application.Utils.Services;

namespace Taime.Application.Services
{
    [InjectionType(InjectionType.Scoped)]
    public class ProductService : BaseService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResultData> GetAll()
        {
            var data = await _productRepository.ReadAsync();
            return SuccessData(data);
        }

        public async Task<ResultData> GetById(int id)
        {
            var data = await _productRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Product_Not_Found);

            return SuccessData(data);
        }

        public async Task<ResultData> Create(ProductEntity request)
        {
            await _productRepository.CreateAsync(request);
            return SuccessData();
        }

        public async Task<ResultData> Remove(int id)
        {
            ProductEntity product = await _productRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Product_Not_Found);

            await _productRepository.RemoveAsync(product);
            return SuccessData();
        }
    }
}