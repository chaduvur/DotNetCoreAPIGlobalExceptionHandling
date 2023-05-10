using GlobalExceptionHandlerMiddleware.DAO;
using GlobalExceptionHandlerMiddleware.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace GlobalExceptionHandlerMiddleware.Extentions
{
    public static class ExceptionMiddlewareExtention
    {
        public static void ConfigureGlobalExceptionHandler(this IApplicationBuilder app) {

            app.UseExceptionHandler(appError => appError.Run(async context=> {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null) {
                    //logger.LogError($"something went wrong: {contextFeature.Error}");
                    byte[] error = Encoding.ASCII.GetBytes(new ErrorDetails() { statusCode = context.Response.StatusCode,message = "Internal Service Error"}.ToString());
                    await context.Response.Body.WriteAsync(error,0,error.Length);
                }
            })); ;
        }

        public static void ConfigureCustomExceptionMiddleware(this  IApplicationBuilder app) {
            app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
