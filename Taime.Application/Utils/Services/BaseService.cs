using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using Taime.Application.Contracts.Shared;

namespace Taime.Application.Utils.Services
{
    public class BaseService
    {
        public ResultData SuccessData() => new ResultData(true);
        public ResultData<T> SuccessData<T>(T data) => new ResultData<T>(data);
        public ResultData ErrorData(Enum metaError) => new ErrorData(metaError);
        public ResultData ErrorData<T>(string metaError) where T : struct => new ErrorData<T>(metaError);
        public ResultData ErrorData<T>(List<string> metaError) where T : struct => new ErrorData<T>(metaError);
        public ResultData ErrorData<TEnum, TRequest>(List<string> metaError) where TEnum : struct where TRequest : class => new ErrorData<TEnum, TRequest>(metaError);
        public ResultData ErrorData<T>(List<T> metaError) where T : struct => new ErrorData<T>(metaError);
        public ResultData ErrorData<TEnum, TRequest>(List<TEnum> metaError) where TEnum : struct where TRequest : class => new ErrorData<TEnum, TRequest>(metaError);
    }
}