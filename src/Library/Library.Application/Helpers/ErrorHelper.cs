using Library.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Helpers
{
    public static class ErrorHelper
    {
        public static bool CheckIfErrorExists(this ErrorResult errorResult, AppErrorCode errorCode)
        {
            foreach (Error error in errorResult.Errors)
            {
                if (error.Details == errorCode.ToString())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
