using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Exceptions.Handlers
{
    /// <summary>
    /// 业务类异常过滤器
    /// </summary>
    public class BusinessExceptionHandler : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            // 判断异常是否BusinessException
            if (context.Exception is BusinessException businessException)
            {
                // 将异常转换成为json结果
                dynamic exceptionResult = new ExpandoObject();
                exceptionResult.Code = businessException.Code;
                exceptionResult.MsgInfo = businessException.MsgInfo;

                if (businessException.Infos != null)
                {
                    exceptionResult.infos = businessException.Infos;
                }
                context.Result = new JsonResult(exceptionResult);
            }
            else
            {
                // 处理其他类型异常Exception
                dynamic exceptionResult = new ExpandoObject();
                exceptionResult.Code = -1;
                exceptionResult.MsgInfo = context.Exception.Message;
                context.Result = new JsonResult(exceptionResult);
            }
        }
    }
}
