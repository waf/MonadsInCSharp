using System;
using System.Collections.Generic;
using System.Text;

namespace MonadsInCSharp
{
    public class Result<TResult>
    {
        readonly internal bool IsSuccess;
        readonly internal TResult success;
        readonly internal string error;

        private Result(bool isOk, TResult success = default, string error = default)
        {
            this.IsSuccess = isOk;
            if (isOk)
            {
                this.success = success;
                this.error = default;
            }
            else
            {
                this.success = default;
                this.error = error;
            }
        }

        public static Result<TResult> Success(TResult value)
        {
            return new Result<TResult>(true, success: value);
        }

        public static Result<TResult> Error(string error)
        {
            return new Result<TResult>(false, error: error);
        }
    }
}
