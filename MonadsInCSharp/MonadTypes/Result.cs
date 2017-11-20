using System;
using System.Collections.Generic;
using System.Text;

namespace MonadsInCSharp
{
    public class Result<TResult>
    {
        readonly internal bool isOk;
        readonly internal TResult ok;
        readonly internal string error;

        private Result(bool isOk, TResult ok = default, string error = default)
        {
            this.isOk = isOk;
            if (isOk)
            {
                this.ok = ok;
                this.error = default;
            }
            else
            {
                this.ok = default;
                this.error = error;
            }
        }

        public static Result<TResult> Ok(TResult value)
        {
            return new Result<TResult>(true, ok: value);
        }

        public static Result<TResult> Error(string error)
        {
            return new Result<TResult>(false, error: error);
        }
    }
}
