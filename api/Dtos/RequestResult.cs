using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class RequestResult<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public Exception Error { get; set; }
        public int StatusCode { get; set; }

        public static RequestResult<T> Success(T data) => new RequestResult<T> { Data = data, IsSuccess = true };
        public static RequestResult<T> Failure(Exception error, int StatusCode) => new RequestResult<T> { IsSuccess = false, Error = error, StatusCode = StatusCode  };
    }
}