using Taime.Application.Contracts.Auth;
using Taime.Application.Contracts.Shared;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Data.MySql.Repositories;
using Taime.Application.Enums;
using Taime.Application.Helpers;
using Taime.Application.Settings;
using Taime.Application.Utils.Services;
using Taime.Application.Validators;

namespace Taime.Application.Services
{
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
            var data = await _userRepository.ReadAsync();
            return SuccessData(data);
        }

        public async Task<ResultData> GetById(int id)
        {
            var data = await _userRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_User_Not_Found);

            return SuccessData(data);
        }

        public async Task<ResultData> Create(UserEntity request)
        {
            UserEntity user = await _userRepository.ReadFirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_User_Already_Exists);

            await _userRepository.CreateAsync(request);
            return SuccessData();
        }

        public async Task<ResultData> Remove(int id)
        {
            UserEntity user = await _userRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_User_Not_Found);

            await _userRepository.RemoveAsync(user);
            return SuccessData();
        }

        public async Task<ResultData> Login(LoginRequest request)
        {
            var validationResult = new LoginRequestValidator().Validate(request);
            if (!validationResult.IsValid)
                return ErrorData<TaimeApiErrors>(validationResult.Errors[0].ErrorCode);

            UserEntity user = await _userRepository.ReadFirstOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password);
            if (user == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_User_Not_Found);

            user.Password = null;

            return SuccessData(AuthorizationHelper.GenerateToken(user, _settings));
        }

        public async Task<ResultData> RefreshToken(RefreshRequest request)
        {
            // TODO
            return await Task.FromResult(SuccessData());
        }
    }
}