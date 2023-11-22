using Taime.Application.Contracts.Shared;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Data.MySql.Repositories;
using Taime.Application.Enums;
using Taime.Application.Utils.Services;

namespace Taime.Application.Services
{
    public class BrandService : BaseService
    {
        private readonly BrandRepository _brandRepository;

        public BrandService(BrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<ResultData> GetAll()
        {
            var data = await _brandRepository.ReadAsync();
            return SuccessData(data);
        }

        public async Task<ResultData> GetById(int id)
        {
            var data = await _brandRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Brand_Not_Found);

            return SuccessData(data);
        }

        public async Task<ResultData> Create(BrandEntity request)
        {
            await _brandRepository.CreateAsync(request);
            return SuccessData();
        }

        public async Task<ResultData> Remove(int id)
        {
            BrandEntity product = await _brandRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Brand_Not_Found);

            await _brandRepository.RemoveAsync(product);
            return SuccessData();
        }
    }
}