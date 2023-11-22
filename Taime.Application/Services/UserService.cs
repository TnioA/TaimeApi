using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Taime.Application.Constants;
using Taime.Application.Contracts.Auth;
using Taime.Application.Contracts.Shared;
using Taime.Application.Contracts.User;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Data.MySql.Repositories;
using Taime.Application.Enums;
using Taime.Application.Extensions;
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
            return SuccessData(data.Select(x => new UserResponse(x)).ToList());
        }

        public async Task<ResultData> GetById(int id)
        {
            var data = await _userRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_User_Not_Found);

            return SuccessData(new UserResponse(data));
        }

        public async Task<ResultData> Create(UserRequest request)
        {
            UserEntity user = await _userRepository.ReadFirstOrDefaultAsync(x => x.Email == request.Email);
            if (user != null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_User_Already_Exists);

            request.Password = StringExtension.HashPassword(request.Password);

            await _userRepository.CreateAsync(new UserEntity(request));
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

            request.Password = StringExtension.HashPassword(request.Password);
            UserEntity user = await _userRepository.ReadFirstOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password);
            if (user == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_User_Not_Found);

            user.Password = null;

            var response = new TokenResponse()
            {
                AccessToken = AuthorizationHelper.GenerateAccessToken(user, _settings),
                ExpiresIn = _settings.JWTAccessTokenExpirationTime,
                RefreshToken = AuthorizationHelper.GenerateRefreshToken(user, _settings),
                TokenType = "Bearer"
            };

            return SuccessData(response);
        }

        public async Task<ResultData> RefreshToken(string userId)
        {
            var convertedUserId = HashIdHelper.Decode(userId);
            UserEntity user = await _userRepository.ReadFirstOrDefaultAsync(x => x.Id == convertedUserId);
            if (user == null)
                return null;

            user.Password = null;

            var response = new TokenResponse()
            {
                AccessToken = AuthorizationHelper.GenerateAccessToken(user, _settings),
                ExpiresIn = _settings.JWTAccessTokenExpirationTime,
                RefreshToken = AuthorizationHelper.GenerateRefreshToken(user, _settings),
                TokenType = AuthConstants.DEFAULT_AUTH_TOKEN_FORMAT
            };

            return SuccessData(response);
        }
    }
}