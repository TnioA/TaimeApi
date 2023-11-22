using Taime.Application.Contracts.Product;
using Taime.Application.Contracts.Shared;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Data.MySql.Repositories;
using Taime.Application.Enums;
using Taime.Application.Utils.Services;

namespace Taime.Application.Services
{
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
            return SuccessData(data.Select(x => new ProductResponse(x)).ToList());
        }

        public async Task<ResultData> GetById(int id)
        {
            var data = await _productRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Product_Not_Found);

            return SuccessData(new ProductResponse(data));
        }

        public async Task<ResultData> Create(ProductRequest request)
        {
            await _productRepository.CreateAsync(new ProductEntity(request));
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