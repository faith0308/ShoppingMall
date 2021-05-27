using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Middlewares
{
    /// <summary>
    /// 请求管道 异常处理 中间件扩展
    /// </summary>
    public static class PipelineExceptionApplicationBuilderExtensions
    {
        /// <summary>
        /// 请求管道 异常处理 中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePipelineExcetion(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<PipelineExceptionHandlerderMiddleware>();
            return builder;
        }
    }
}
