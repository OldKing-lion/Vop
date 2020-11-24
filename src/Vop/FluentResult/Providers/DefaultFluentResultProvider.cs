﻿using Microsoft.AspNetCore.Mvc;
using System;
using Vop.Api.FluentException;

namespace Vop.Api.FluentResult
{
    public class DefaultFluentResultProvider : IFluentResultProvider
    {
        /// <summary>
        /// 异常返回值
        /// </summary>
        public virtual object OnException(Exception exception)
        {
            ApiException exp = ResolveException(exception);
            Output output = new Output()
            {
                Code = 0,
                Msg = exp.Msg,
                ErrCode = exp.Code,
                ErrMsg = exp.Message,
            };
            return output;
        }

        /// <summary>
        /// 成功返回值
        /// </summary>
        public virtual IActionResult OnSuccessed(object data)
        {
            Output output = new Output()
            {
                Code = 1,
                Msg = "操作成功",
                Data = data
            };
            return new JsonResult(output);
        }

        private ApiException ResolveException(Exception exception)
        {
            if (exception is ApiException) return exception as ApiException;
            else if (exception.InnerException != null && exception.InnerException is ApiException) return exception.InnerException as ApiException;
            else if (exception.InnerException != null && exception.InnerException.InnerException != null && exception.InnerException.InnerException is ApiException) return exception.InnerException.InnerException as ApiException;
            else return new ApiUnknowException(exception.Message, exception);
        }

    }
}