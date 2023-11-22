using Taime.Application.Contracts.Shared;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Data.MySql.Repositories;
using Taime.Application.Enums;
using Taime.Application.Utils.Services;

namespace Taime.Application.Services
{
    public class CategoryService : BaseService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResultData> GetAll()
        {
            var data = await _categoryRepository.ReadAsync();
            return SuccessData(data);
        }

        public async Task<ResultData> GetById(int id)
        {
            var data = await _categoryRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Category_Not_Found);

            return SuccessData(data);
        }

        public async Task<ResultData> Create(CategoryEntity request)
        {
            await _categoryRepository.CreateAsync(request);
            return SuccessData();
        }

        public async Task<ResultData> Remove(int id)
        {
            CategoryEntity product = await _categoryRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Category_Not_Found);

            await _categoryRepository.RemoveAsync(product);
            return SuccessData();
        }
    }
}