using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quanzk.Commons.Middlewares
{
    /// <summary>
    /// 请求管道异常处理中间件 拦截Response输出流，进行统一异常处理
    /// </summary>
    public class PipelineExceptionHandlerderMiddleware
    {
        /// <summary>
        /// 定义请求管道委托
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        public PipelineExceptionHandlerderMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// 执行异常处理(执行action无法拦截的异常)
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, context.Response.StatusCode, ex.Message);
            }
        }

        /// <summary>
        /// 异常处理 包装
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <param name="statuCode">状态码</param>
        /// <param name="msg">异常信息</param>
        /// <returns></returns>
        private async static Task HandleExceptionAsync(HttpContext context, int statuCode, string msg)
        {
            context.Response.ContentType = "application/json;charset=utf-8";
            // 异常结果转换成json格式输出
            dynamic wrapResult = new ExpandoObject();
            wrapResult.Code = "-1";
            wrapResult.MsgInfo = msg;

            // 异常json格式输出
            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, wrapResult);
        }
    }
}
