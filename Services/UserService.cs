using System.Threading.Tasks;
using TaimeApi.Data.MySql.Entities;
using TaimeApi.Data.MySql.Repositories;
using TaimeApi.Enums;
using TaimeApi.Utils.Attributes;
using TaimeApi.Utils.Services;

namespace TaimeApi.Services
{
    [InjectionType(InjectionType.Scoped)]
    public class UserService : BaseService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultData> GetAll()
        {
            var data = await _userRepository.ReadAsync(x=> x.IsAdmin == false);
            return SuccessData(data);
        }

        public async Task<ResultData> GetById(int id)
        {
            if (id < 1)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Invalid_Id);

            return SuccessData(await _userRepository.ReadFirstOrDefaultAsync(x=> x.Id == id));
        }

        public async Task<ResultData> Create(UserEntity request)
        {
            await _userRepository.CreateAsync(request);
            return SuccessData(request);
        }
    }
}