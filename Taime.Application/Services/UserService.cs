using System.Threading.Tasks;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Data.MySql.Repositories;
using Taime.Application.Enums;
using Taime.Application.Helpers;
using Taime.Application.Settings;
using Taime.Application.Utils.Attributes;
using Taime.Application.Utils.Helpers;
using Taime.Application.Utils.Services;

namespace Taime.Application.Services
{
    [InjectionType(InjectionType.Scoped)]
    public class UserService : BaseService
    {
        private readonly UserRepository _userRepository;
        private readonly AppSettings _settings;

        public UserService(UserRepository userRepository, AppSettings settings)
        {
            _userRepository = userRepository;
            _settings = settings;
        }

        public async Task<ResultData> GetAll()
        {
            var data = await _userRepository.ReadAsync(x=> x.IsAdmin == false);
            return SuccessData(data);
        }

        public async Task<ResultData> Login(string email, string password)
        {
            if (email == null || password == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Invalid_Login);

            UserEntity user = await _userRepository.ReadFirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            if (user == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_User_Not_Finded);

            user.Password = null;
            user.Token = AuthorizationHelper.GenerateToken(user, _settings);

            return SuccessData(user);
        }

        public async Task<ResultData> Create(UserEntity request)
        {
            await _userRepository.CreateAsync(request);
            return SuccessData(request);
        }

        public async Task<ResultData> Remove(int id)
        {
            UserEntity user = await _userRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_User_Not_Finded);

            await _userRepository.RemoveAsync(user);
            return SuccessData();
        }
    }
}