using Taime.Application.Contracts.Collection;
using Taime.Application.Contracts.Shared;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Data.MySql.Repositories;
using Taime.Application.Enums;
using Taime.Application.Utils.Services;

namespace Taime.Application.Services
{
    public class CollectionService : BaseService
    {
        private readonly CollectionRepository _collectionRepository;

        public CollectionService(CollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        public async Task<ResultData> GetAll()
        {
            var data = await _collectionRepository.ReadAsync();
            return SuccessData(data.Select(x => new CollectionResponse(x)).ToList());
        }

        public async Task<ResultData> GetById(int id)
        {
            var data = await _collectionRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Collection_Not_Found);

            return SuccessData(new CollectionResponse(data));
        }

        public async Task<ResultData> Create(CollectionRequest request)
        {
            await _collectionRepository.CreateAsync(new CollectionEntity(request));
            return SuccessData();
        }

        public async Task<ResultData> Remove(int id)
        {
            CollectionEntity product = await _collectionRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Collection_Not_Found);

            await _collectionRepository.RemoveAsync(product);
            return SuccessData();
        }
    }
}