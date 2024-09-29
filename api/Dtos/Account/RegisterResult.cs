using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Dtos.Account
{
    public class RegisterResult
    {
        public NewUserDto User { get; set; }
        public bool IsSuccess => User != null;
        public IEnumerable<IdentityError> Errors  { get; set; }
        public int StatusCode { get; set; }

        public static RegisterResult Success(NewUserDto user)
        {
            return new RegisterResult { User = user, StatusCode = 201 };
        }
        public static RegisterResult Failure(IEnumerable<IdentityError> errors, int statusCode)
        {
            return new RegisterResult { Errors = errors, StatusCode = statusCode };
        }

    }
}