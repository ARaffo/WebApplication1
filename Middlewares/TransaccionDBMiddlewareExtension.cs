using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Middlewares
{
    public static class TransaccionDBMiddlewareExtension
    {
        public static IApplicationBuilder UseTransaccionDBMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TransaccionDBMiddleware>();

        }
    }
}
