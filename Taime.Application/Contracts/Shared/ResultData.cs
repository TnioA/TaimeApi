using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using Taime.Application.Extensions;
using Taime.Application.Helpers;

namespace Taime.Application.Contracts.Shared
{
    public class Error
    {
        public string Code { get; set; }

        public string Message { get; set; }
    }

    public class MetaError
    {
        public Error Error { get; set; }

        public int? ProtocolCode { get; set; }

        public MetaError(Error error, int? protocolCode)
        {
            Error = error;
            ProtocolCode = protocolCode;
        }
    }

    public class ResultData
    {
        public virtual bool Success { get; }

        public static implicit operator ResultData(Enum error) => new ErrorData(error);
        public static implicit operator ActionResult(ResultData resultData) => HttpHelper.Convert(resultData);

        public ResultData(bool success)
        {
            Success = success;
        }
    }

    public interface ISuccessResultData
    {
        bool Success { get; }

        object Data { get; }
    }

    public class ResultData<T> : ResultData, ISuccessResultData
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public T Data { get; private set; }

        object ISuccessResultData.Data => Data;

        public static implicit operator ResultData<T>(T data) => new ResultData<T>(data);
        public static implicit operator ActionResult(ResultData<T> resultData) => HttpHelper.Convert(resultData);
        public static implicit operator ResultData<T>(Enum error) => new ResponseErrorData<T>(error);
        public static implicit operator ResultData<T>((Enum, object) error) => new ResponseErrorData<T>(error.Item1, error.Item2);

        public ResultData() : base(false) { }
        public ResultData(T data) : base(true)
        {
            Data = data;
        }
    }

    public interface IErrorData
    {
        List<Error> Errors { get; }
    }

    public class ErrorData : ResultData, IErrorData
    {
        public List<Error> Errors { get; private set; }

        public ErrorData() : base(false)
        {
            Errors = new List<Error>();
        }

        public ErrorData(Enum errorCode) : this()
        {
            BindErrors(new[] { errorCode });
        }

        public ErrorData(IEnumerable<Enum> errorCodes) : this()
        {
            BindErrors(errorCodes);
        }

        protected virtual void BindErrors<T>(IEnumerable<string> errorsCode, object paramReplace = null) where T : struct
        {
            foreach (var item in errorsCode)
            {
                var errorDescription = Enum.Parse(typeof(T), item).ErrorDescription(item);

                ReplaceMessage(errorDescription.Error, paramReplace);

                Errors.Add(errorDescription.Error);
            }
        }

        protected virtual void BindErrors<T>(IEnumerable<T> errorsCode, object paramReplace = null) where T : struct
        {
            foreach (var item in errorsCode)
            {
                var errorDescription = item.ErrorDescription();

                ReplaceMessage(errorDescription.Error, paramReplace);

                Errors.Add(errorDescription.Error);
            }
        }

        protected virtual void BindErrors(IEnumerable<Enum> errorsCode, object paramReplace = null)
        {
            BindErrors(this, errorsCode, paramReplace);
        }

        public static void BindErrors(IErrorData errorData, IEnumerable<Enum> errorsCode, object paramReplace = null)
        {
            foreach (var item in errorsCode)
            {
                var errorDescription = item.ErrorDescription();

                ReplaceMessage(errorDescription.Error, paramReplace);

                errorData.Errors.Add(errorDescription.Error);
            }
        }

        public static void ReplaceMessage(Error error, object paramReplace)
        {
            if (paramReplace != null)
            {
                if (paramReplace.GetType() == typeof(object[]))
                {
                    var paramsReplace = (object[])paramReplace;
                    for (int i = 0; i < paramsReplace.Count(); i++)
                    {
                        error.Message = error.Message.Replace($"[{i}]", (paramsReplace[i] ?? "").ToString());
                    }
                }
                else
                {
                    error.Message = error.Message.Replace("[0]", (paramReplace ?? "").ToString());
                }
            }
        }
    }

    public class ErrorData<T> : ErrorData where T : struct
    {
        public ErrorData() : base() { }

        public ErrorData(List<string> errorsCode) : base()
        {
            BindErrors<T>(errorsCode);
        }

        public ErrorData(string errorCode) : base()
        {
            BindErrors<T>(new List<string>() { errorCode });
        }

        public ErrorData(Enum errorCode) : base(errorCode) { }

        public ErrorData(IEnumerable<Enum> errorCodes) : base(errorCodes) { }

        public ErrorData(T errorCode) : base()
        {
            BindErrors(new[] { errorCode });
        }

        public ErrorData(List<T> errorCode) : base()
        {
            BindErrors(errorCode);
        }

        public ErrorData(T errorCode, object paramReplace) : base()
        {
            BindErrors(new[] { errorCode }, paramReplace);
        }

        public ErrorData(string errorCode, object paramReplace) : base()
        {
            BindErrors<T>(new[] { errorCode }, paramReplace);
        }
    }

    public class ResponseErrorData<T> : ResultData<T>, IErrorData
    {
        public ResponseErrorData() : base()
        {
            Errors = new List<Error>();
        }

        public ResponseErrorData(Enum errorCode) : this()
        {
            ErrorData.BindErrors(this, new[] { errorCode });
        }

        public ResponseErrorData(IEnumerable<Enum> errorCodes) : this()
        {
            ErrorData.BindErrors(this, errorCodes);
        }

        public ResponseErrorData(Enum errorCode, object paramReplace) : this()
        {
            ErrorData.BindErrors(this, new[] { errorCode }, paramReplace);
        }

        public List<Error> Errors { get; private set; }
    }

    public class ErrorData<TEnum, TRequest> : ErrorData<TEnum> where TEnum : struct where TRequest : class
    {
        private static readonly IDictionary<string, PropertyInfo> properties = typeof(TRequest).GetProperties().ToDictionary(i => i.Name);

        public ErrorData(List<string> errorsCode) : base()
        {
            BindErrors<TEnum>(errorsCode);
        }

        public ErrorData(string errorCode) : base()
        {
            BindErrors<TEnum>(new[] { errorCode });
        }

        public ErrorData(TEnum errorCode) : base()
        {
            BindErrors(new[] { errorCode });
        }

        public ErrorData(List<TEnum> errorCode) : base()
        {
            BindErrors(errorCode);
        }

        public ErrorData(TEnum errorCode, object paramReplace) : base()
        {
            BindErrors(new[] { errorCode }, paramReplace);
        }

        public ErrorData(string errorCode, object paramReplace) : base()
        {
            BindErrors<TEnum>(new[] { errorCode }, paramReplace);
        }

        protected override void BindErrors<T>(IEnumerable<string> errorsCode, object paramReplace = null) where T : struct
        {
            foreach (var item in errorsCode)
            {
                var errorDescription = Enum.Parse<T>(item).ErrorDescription(properties);

                ReplaceMessage(errorDescription.Error, paramReplace);

                Errors.Add(errorDescription.Error);
            }
        }

        protected override void BindErrors<T>(IEnumerable<T> errorsCode, object paramReplace = null) where T : struct
        {
            foreach (var item in errorsCode)
            {
                var errorDescription = item.ErrorDescription(properties);

                ReplaceMessage(errorDescription.Error, paramReplace);

                Errors.Add(errorDescription.Error);
            }
        }
    }
}
