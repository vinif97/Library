using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Result
{
    public class SuccessResult : Result
    {
        public SuccessResult()
        {
            IsSuccess = true;
        }
    }

    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T data) : base(data)
        {
            IsSuccess = true;
        }
    }
}
