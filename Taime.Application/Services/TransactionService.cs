using Microsoft.AspNetCore.Mvc;
using Taime.Application.Constants;
using Taime.Application.Contracts.Auth;
using Taime.Application.Contracts.Shared;
using Taime.Application.Contracts.Transaction;
using Taime.Application.Contracts.User;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Data.MySql.Repositories;
using Taime.Application.Enums;
using Taime.Application.Helpers;
using Taime.Application.Settings;
using Taime.Application.Utils.Services;
using Taime.Application.Validators;

namespace Taime.Application.Services
{
    public class TransactionService : BaseService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly UserRepository _userRepository;
        private readonly AppSettings _settings;

        public TransactionService(TransactionRepository transactionRepository, 
                                  UserRepository userRepository,
                                  AppSettings settings)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;   
            _settings = settings;
        }

        public async Task<ResultData> GetAll()
        {
            var data = await _transactionRepository.ReadAsync();
            return SuccessData(data.Select(x => new TransactionResponse(x)).ToList());
        }

        public async Task<ResultData> GetById(int id)
        {
            var data = await _transactionRepository.ReadFirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Transaction_Not_Found);

            return SuccessData(new TransactionResponse(data));
        }

        public async Task<ResultData> Create(TransactionRequest request, string userId)
        {
            var convertedUserId = HashIdHelper.Decode(userId);
            UserEntity user = await _userRepository.ReadFirstOrDefaultAsync(x => x.Id == convertedUserId);
            if (user != null)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_User_Not_Found);


            // TODO
            // ...


            var newTransaction = new TransactionEntity();
            await _transactionRepository.CreateAsync(newTransaction);
            return SuccessData();
        }
    }
}